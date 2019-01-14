package com.avans.tentamenmanager;

import com.google.api.client.auth.oauth2.Credential;
import com.google.api.client.extensions.java6.auth.oauth2.AuthorizationCodeInstalledApp;
import com.google.api.client.extensions.jetty.auth.oauth2.LocalServerReceiver;
import com.google.api.client.googleapis.auth.oauth2.GoogleAuthorizationCodeFlow;
import com.google.api.client.googleapis.auth.oauth2.GoogleClientSecrets;
import com.google.api.client.googleapis.javanet.GoogleNetHttpTransport;
import com.google.api.client.http.javanet.NetHttpTransport;
import com.google.api.client.json.JsonFactory;
import com.google.api.client.json.jackson2.JacksonFactory;
import com.google.api.client.util.store.FileDataStoreFactory;
import com.google.api.services.sheets.v4.Sheets;
import com.google.api.services.sheets.v4.SheetsScopes;
import com.google.api.services.sheets.v4.model.*;
import freemarker.template.Template;
import freemarker.template.TemplateException;
import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.io.*;
import java.nio.file.FileAlreadyExistsException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.security.GeneralSecurityException;
import java.util.*;

public class GoogleSheetOrganizer {
    private static final String APPLICATION_NAME = "TentamenManager";
    private static final JsonFactory JSON_FACTORY = JacksonFactory.getDefaultInstance();
    private static final String TOKENS_DIRECTORY_PATH = "tokens";
    private static final List<String> SCOPES = Collections.singletonList(SheetsScopes.SPREADSHEETS);
    private static final String CREDENTIALS_FILE_PATH = "/client_id.json";


    public String spreadsheetId = "1A8dVicV07-sKUW2yqUpyEHPM0VmV7ALN9ae6Il3WRcE";
    public String overviewName = "Tentamen Overview";
    public String testSheetName = "Tentamen Tests";
    public String manualSheetName = "Tentamen Handmatig";
    public String studentlistSheetName = "Studentenlijst";
    public String idColumn = "A";
    public String firstnameColumn = "E";
    public String lastnameColumn = "H";


    final NetHttpTransport HTTP_TRANSPORT = GoogleNetHttpTransport.newTrustedTransport();
    private final Sheets service;
    private final TestManager testManager;

    private List<String> exercises;



    public GoogleSheetOrganizer(TestManager testManager) throws GeneralSecurityException, IOException {
        this.testManager = testManager;

        if(exercises == null)
            buildExercises();

        service = new Sheets.Builder(HTTP_TRANSPORT, JSON_FACTORY, getCredentials(HTTP_TRANSPORT)).setApplicationName(APPLICATION_NAME).build();
    }

    public void buildManualCorrection() throws IOException {
        if(checkSheetExists(manualSheetName, false))
        {
            Alert alert = new Alert(Alert.AlertType.CONFIRMATION, "This sheet already exists. Are you sure you want to overwrite?", ButtonType.YES, ButtonType.NO);
            Optional<ButtonType> result = alert.showAndWait();
            if(!result.isPresent() || result.get() == ButtonType.NO)
                return;
        }
        else
            findOrCreateSheet(manualSheetName);

        System.out.println("Building test result sheet");
        service.spreadsheets().values().clear(spreadsheetId, manualSheetName, new ClearValuesRequest()).execute();

        final List<List<Object>> values = new ArrayList<>();
        //add header
        {
            List<Object> header = new ArrayList<>();
            header.add("StudentNumber");
            header.add("First name");
            header.add("Last name");
            for(String ex : exercises)
            {
                header.add(ex);
                header.add("Correctie punten");
                header.add("Reden");
            }
            header.add("Total test");
            header.add("Correction");
            values.add(header);
        }

        testManager.getStudents().forEach(s ->
        {
            List<Object> row = new ArrayList<>();
            //first rows are studentname, first name, last name
            row.add(s.getStudentId());
            row.add("=VLOOKUP($A"+(values.size()+1)+", "+quoteSheetName(studentlistSheetName)+"!$A$1:$Z$999, "+(getColIndex(firstnameColumn))+", false)");
            row.add("=VLOOKUP($A"+(values.size()+1)+", "+quoteSheetName(studentlistSheetName)+"!$A$1:$Z$999, "+(getColIndex(lastnameColumn))+", false)");


            JSONArray log = s.getLog();
            JSONObject lastLog = (JSONObject) log.get(log.size()-1);
            JSONObject testResults = (JSONObject) lastLog.get("test");

            //3 cols per exercise
            JSONObject scores = ((JSONObject)testResults.get("scores"));
            for(String ex : exercises)
            {
                if(scores.containsKey(ex))
                    row.add(scores.get(ex));
                else
                    row.add(0);

                row.add("");
                row.add("");
            }
            row.add(testResults.get("score"));

            String test = "=0";
            for(int i = 0; i < exercises.size(); i++)
                test += "+IF(ISNUMBER(" + getColName(5+i*3) + (values.size()+1) + ")," + getColName(5+i*3) + (values.size()+1) + " - " +getColName(4+i*3) + (values.size()+1) + ",0)";

            row.add(test);

            values.add(row);
        });

        service.spreadsheets().values().append(spreadsheetId, manualSheetName, new ValueRange().setValues(values)).setValueInputOption("USER_ENTERED").execute();


        Sheet sheet = findSheet(manualSheetName);


        ArrayList<Request> requests = new ArrayList<>();

        //set column width
        requests.add(new Request().setUpdateDimensionProperties(new UpdateDimensionPropertiesRequest()
            .setRange(new DimensionRange()
                            .setSheetId(sheet.getProperties().getSheetId())
                            .setDimension("COLUMNS")
                            .setStartIndex(3)
                            .setEndIndex(3+exercises.size()*3))
                .setFields("pixelSize")
                .setProperties(new DimensionProperties().setPixelSize(20))));


        for(int i = 0; i < exercises.size(); i++)
            requests.add(new Request().setRepeatCell(new RepeatCellRequest()
                    .setRange(new GridRange()
                            .setSheetId(sheet.getProperties().getSheetId())
                            .setStartRowIndex(1)
                            .setStartColumnIndex(3+3*i)
                            .setEndColumnIndex(4+3*i))
                    .setCell(new CellData()
                        .setUserEnteredFormat(new CellFormat()
    //                    .setTextFormatRuns(Arrays.asList(new TextFormatRun().setFormat(new TextFormat().setForegroundColor(new Color().setRed(1.0f).setGreen(1.0f).setBlue(1.0f)))))
                        .setBackgroundColor(new Color().setRed(0.5f).setGreen(0.5f).setBlue(0.5f))))
                    .setFields("userEnteredFormat(backgroundColor,textFormat)")));



        BatchUpdateSpreadsheetRequest busReq = new BatchUpdateSpreadsheetRequest();
        busReq.setRequests(requests);
        service.spreadsheets().batchUpdate(spreadsheetId, busReq).execute();

    }


    /**
     * fills the list of exercises by scanning all students
     */
    public void buildExercises() {
        System.out.print("Building list of exercises...");
        exercises = new ArrayList<String>();
        testManager.getStudents().forEach(s ->
        {
            JSONArray log = s.getLog();
            JSONObject lastLog = (JSONObject) log.get(log.size()-1);
            JSONObject testResults = (JSONObject) lastLog.get("test");
            JSONObject scoresJson = ((JSONObject)testResults.get("scores"));
            List<String> scores = new ArrayList<>(scoresJson.keySet());
            for(String score : scores)
                if(!exercises.contains(score) && !score.equals("TestStudentNumber"))
                    exercises.add(score);

        });
        Collections.sort(exercises);
        System.out.println(exercises.size() + " found");
    }

    public void buildOverview() throws IOException {
        List<List<Object>> field = new ArrayList<>();

        if(!checkSheetExists(studentlistSheetName) || !checkSheetExists(manualSheetName) || !checkSheetExists(testSheetName))
            return;

        int studentCount = service.spreadsheets().values().get(spreadsheetId, studentlistSheetName).execute().getValues().size();
        System.out.println(studentCount + " students found");


        int theoryCount = service.spreadsheets().values().get(spreadsheetId, "Tentamen Theorie").execute().getValues().get(0).size();
        int testRowCount = service.spreadsheets().values().get(spreadsheetId, testSheetName).execute().getValues().get(0).size();
        int manualRowCount = service.spreadsheets().values().get(spreadsheetId, manualSheetName).execute().getValues().get(0).size();


        for(int i = 0; i < studentCount+1; i++)
            field.add(i, new ArrayList(Collections.nCopies(10, "")));


        findOrCreateSheet(overviewName);
        service.spreadsheets().values().clear(spreadsheetId, overviewName, new ClearValuesRequest()).execute();

        field.get(0).set(0, "Studentnummer");
        field.get(0).set(1, "Voornaam");
        field.get(0).set(2, "Achternaam");
        field.get(0).set(3, "Automatische test");
        field.get(0).set(4, "Handmatige correctie");
        field.get(0).set(5, "Theorie");
        field.get(0).set(6, "Totaal");
        field.get(0).set(7, "Cijfer");
        field.get(0).set(8, "Opnieuw nakijken?");
        field.get(0).set(9, "Nagekeken door");

        field.get(1).set(0, "=QUERY(Studentenlijst!A2:H999,\"select A where A > 0 order by H\")");



        for(int i = 1; i < studentCount; i++) {
            field.get(i).set(1, "=VLOOKUP($A"+(i+1)+", Studentenlijst!$A$1:$H$999, 5, false)");
            field.get(i).set(2, "=VLOOKUP($A"+(i+1)+", Studentenlijst!$A$1:$H$999, 8, false)");
            field.get(i).set(3, addNumberCheck("VLOOKUP($A"+(i+1)+", 'Tentamen Tests'!$A$1:$ZZ$999, "+testRowCount+", false)", "NA"));
            field.get(i).set(4, addNumberCheck("VLOOKUP($A"+(i+1)+", 'Tentamen Handmatig'!$A$1:$ZZ$999, "+manualRowCount+", false)", "NA"));
            field.get(i).set(5, addNumberCheck("VLOOKUP($A"+(i+1)+", 'Tentamen Theorie'!$A$1:$ZZ$999, "+theoryCount+", false)", "NA"));
            field.get(i).set(6, "=IF(ISNUMBER(D" + (i+1) + "), D" + (i+1) + ", 0) "+
                            "+ IF(ISNUMBER(E" + (i+1) + "), E" + (i+1) + ", 0) "+
                            "+ IF(ISNUMBER(F" + (i+1) + "), F" + (i+1) + ", 0) "+
                    "");
            field.get(i).set(7, "=ROUND(G" + (i+1) + "/20, 1)");
        }

        service.spreadsheets().values().update(spreadsheetId, overviewName, new ValueRange().setValues(field)).setValueInputOption("USER_ENTERED").execute();

    }


    /**
     * Builds a sheet with test results
     * @throws IOException
     */
    public void buildTestResultSheet() throws IOException {

        if(checkSheetExists(testSheetName))
        {
            Alert alert = new Alert(Alert.AlertType.CONFIRMATION, "This sheet already exists. Are you sure you want to overwrite?", ButtonType.YES, ButtonType.NO);
            Optional<ButtonType> result = alert.showAndWait();
            if(!result.isPresent() || result.get() == ButtonType.NO)
                return;
            findOrCreateSheet(testSheetName);
        }

        service.spreadsheets().values().clear(spreadsheetId, testSheetName, new ClearValuesRequest()).execute();

        final List<List<Object>> values = new ArrayList<>();
        //add header
        {
            List<Object> header = new ArrayList<>();
            header.add("StudentNumber");
            header.add(""); //add last name here
            for(String ex : exercises)
            {
                header.add(ex);
                header.add(ex + "_errors");
            }
            header.add("Total");
            values.add(header);
        }

        testManager.getStudents().forEach(s ->
        {
            List<Object> row = new ArrayList<>();
            row.add(s.getStudentId());
		        row.add("=VLOOKUP($A"+(values.size()+1)+", "+quoteSheetName(studentlistSheetName)+"!$A$1:$Z$999, "+(getColIndex(lastnameColumn))+", false)");

            JSONArray log = s.getLog();
            JSONObject lastLog = (JSONObject) log.get(log.size()-1);
            JSONObject testResults = (JSONObject) lastLog.get("test");

            JSONObject scores = ((JSONObject)testResults.get("scores"));
            JSONObject errors = ((JSONObject)testResults.get("errors"));

            for(String ex : exercises)
            {
                if(scores.containsKey(ex))
                    row.add(scores.get(ex));
                else
                    row.add(0);

                String error = "";
                for(Object key : errors.keySet())
                {
                    if(((String)key).startsWith(ex))
                        error += errors.get(key) + "\n";
                }

                row.add(error);

            }

            row.add(testResults.get("score"));

            values.add(row);
        });

        service.spreadsheets().values().append(spreadsheetId, testSheetName, new ValueRange().setValues(values)).setValueInputOption("USER_ENTERED").execute();
    }

    private Sheet findOrCreateSheet(String title) throws IOException {
        Sheet sheet = findSheet(title);
        if(sheet == null)
        {
            buildSheet(title);
            sheet = findSheet(title);
        }
        return sheet;
    }

    private Sheet findSheet(String title) throws IOException {
        final Spreadsheet sheets = service.spreadsheets().get(spreadsheetId).execute();
        for (Sheet sheet : sheets.getSheets())
            if(sheet.getProperties().getTitle().toLowerCase().equals(title.toLowerCase()))
                return sheet;
        return null;
    }

    private void buildSheet(String title) throws IOException {
        List<Request> requests = new ArrayList<>();

        requests.add(new Request().setAddSheet(new AddSheetRequest().setProperties(new SheetProperties().setTitle(title))));
        BatchUpdateSpreadsheetRequest request = new BatchUpdateSpreadsheetRequest().setRequests(requests);
        service.spreadsheets().batchUpdate(spreadsheetId, request).execute();
    }

    /**
     * Checks if a sheet exists and spits out a UI error message
     * @param sheetName
	 * @param showAlert
     * @return true if found, false if not found
     */
    private boolean checkSheetExists(String sheetName, boolean showAlert) {
        try
        {
            service.spreadsheets().values().get(spreadsheetId, sheetName).execute().getValues().size();
        }
        catch(Exception e)
        {
            showError("Could not find sheet '" + sheetName + "'");
            return false;
        }
        return true;
    }

    private boolean checkSheetExists(String sheetName)
	{
		return checkSheetExists(sheetName, true);
	}



    /**
     * Just shows an error message
     * @param message message
     */
    private void showError(String message) {
        Alert alert = new Alert(Alert.AlertType.ERROR, message, ButtonType.OK);
        alert.showAndWait();
    }

    /**
     * Changes a formula to add an isNumber check. If the value is not a valid number, isNaN value will be used as string
     * @param query
     * @param ifNaN
     * @return
     */
    private String addNumberCheck(String query, String ifNaN)
    {
        return "=IF(ISNUMBER(" + query + "), " + query + ", \"" + ifNaN + "\")";
    }


    /**
     * Adds single quotes around a sheet name if needed, for in A1 functions
     * @param sheetName
     * @return a quoted string
     */
    private String quoteSheetName(String sheetName)
    {
        if(sheetName.contains(" "))
            return "'" + sheetName + "'";
        return sheetName;
    }


    /**
     * Returns the column of a column index.
     * @param col column index, 1-indexed
     * @return a column name (A to Z, then AA to AZ, etc)
     */
    public String getColName(int col) {
        final String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        String ret = "";
        while(col != 0)
        {
            ret = alphabet.charAt((col-1)%26) + ret;
            col -= (col-1)%26;
            col /= 26;
        }

        return ret;
    }

    /**
     * Parses a column name to a column index
     * @param col the column name
     * @return a column index, 1-indexed
     */
    public int getColIndex(String col) {
        final String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int column = 0;
        for(int i = 0; i < col.length(); i++)
        {
            column *= 26;
            column += alphabet.indexOf(col.charAt(i)) +1;
        }

        return column;
    }


    /**
     * Creates an authorized Credential object.
     *
     * @param HTTP_TRANSPORT The network HTTP Transport.
     * @return An authorized Credential object.
     * @throws IOException If the credentials.json file cannot be found.
     */
    private static Credential getCredentials(final NetHttpTransport HTTP_TRANSPORT) throws IOException {
        // Load client secrets.
        InputStream in = GoogleSheetOrganizer.class.getResourceAsStream(CREDENTIALS_FILE_PATH);
        GoogleClientSecrets clientSecrets = GoogleClientSecrets.load(JSON_FACTORY, new InputStreamReader(in));

        // Build flow and trigger user authorization request.
        GoogleAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow.Builder(
                HTTP_TRANSPORT, JSON_FACTORY, clientSecrets, SCOPES)
                .setDataStoreFactory(new FileDataStoreFactory(new java.io.File(TOKENS_DIRECTORY_PATH)))
                .setAccessType("offline")
                .build();
        LocalServerReceiver receiver = new LocalServerReceiver.Builder().setPort(8888).build();
        return new AuthorizationCodeInstalledApp(flow, receiver).authorize("user");
    }

    public static void main(String[] args) throws GeneralSecurityException, IOException {

        TestManager testManager = new TestManager();
        testManager.setPath("D:\\avans\\Kwartalen\\Voltijd TI\\1.2 Voltijd\\2018-2019\\OGP1\\Tentamen\\work2");
        new GoogleSheetOrganizer(testManager);
    }


    private List<Object> findRow(List<List<Object>> data, String id, int idColumn)
	{

		for(List<Object> r : data)
			if(!r.isEmpty() && r.get(idColumn).equals(id))
				return r;
		return null;
	}

    public void buildReports() throws IOException, TemplateException {

		List<List<Object>> overview = service.spreadsheets().values().get(spreadsheetId, overviewName).execute().getValues();
		List<List<Object>> theory = service.spreadsheets().values().get(spreadsheetId, "Tentamen Theorie").execute().getValues();
		List<List<Object>> testSheet = service.spreadsheets().values().get(spreadsheetId, testSheetName).execute().getValues();
		List<List<Object>> manualSheet = service.spreadsheets().values().get(spreadsheetId, manualSheetName).execute().getValues();

		for(List<Object> row : overview)
		{
			try
			{
				Integer.parseInt((String) row.get(0));
			} catch(NumberFormatException e) {
				continue;
			} catch(IndexOutOfBoundsException e) {
				continue;
			}

			System.out.println("Generating " + row.get(0));

			Template template = TemplateConfiguration.getConfig().getTemplate("template.tex");
			Map<String, Object> root = new HashMap<String, Object>();

			root.put("Student", new HashMap<String, Object>()
			{{
				put("number", row.get(0));
				put("name", row.get(1) + " " + row.get(2));
			}});

			root.put("Grade", row.get(7));
			root.put("TotalPoints", row.get(6));
			root.put("ManualCorrector", row.size() > 8 ? row.get(9) : "");

			List<Object> allMc = new ArrayList<>();
			root.put("mc", allMc);

			List<Object> allOpen = new ArrayList<>();
			root.put("open", allOpen);

			List<Object> questions = new ArrayList<>();
			root.put("questions", questions);


			final List<Object> theoryRow = findRow(theory, (String)row.get(0), 0);
			final List<Object> testRow = findRow(testSheet, (String)row.get(0), 0);
			final List<Object> manualRow = findRow(manualSheet, (String)row.get(0), 0);

			if(manualRow == null)
			{
				System.out.println("No manual correction found for " + row.get(0));
				continue;
			}


			for(int mc = 0; mc < 15; mc++) //todo: softconfig mc range
			{
				final int _mc = mc;
				allMc.add(new HashMap<String, Object>()
				{{
					put("question", "abcdefghijklmnopqrstuvwxyz".charAt(_mc) + "");
					put("answer", theoryRow.get(4 + _mc));
					put("correctanswer", theory.get(1).get(4+_mc));
					put("score", theoryRow.get(4 + _mc).toString().toLowerCase().equals(theory.get(1).get(4+_mc).toString().toLowerCase()) ? theory.get(2).get(4+_mc) : "0");
				}});
			}

			for(int o = 0; o < 5; o++) //todo: softconfig open questions range
			{
				final int _o = o;
				allOpen.add(new HashMap<String, Object>()
				{{
					put("question", "abcdefghijklmnopqrstuvwxyz".charAt(_o + 15) + "");
					put("score", theoryRow.get(4+15+1+2*_o));
					put("reason", latexEscape((String)theoryRow.get(4+15+1+2*_o+1)));
				}});
			}


			for(int q = 0; q < (testRow.size()-2)/2; q++)
			{
				final int _q = q;
				questions.add(new HashMap<String, Object>()
				{{
					put("question", testSheet.get(0).get(2+2*_q));
					put("testScore", testRow.get(2+2*_q));
					put("testErrors", latexEscape((String)testRow.get(2+2*_q+1)));
					put("manualScore", manualRow.get(3+3*_q+1));
					put("manualReason", latexEscape((String) manualRow.get(3+3*_q+2)));
				}});
			}





//			Writer out = new OutputStreamWriter(System.out);
			Writer out = new OutputStreamWriter(new FileOutputStream("reports/template/out.tex"));
			template.process(root, out);



			ProcessBuilder builder = new ProcessBuilder("pdflatex", "out.tex");
			builder.directory(new File("reports/template"));
			Process process = builder.start();
			InputStream is = process.getInputStream();
			BufferedReader reader = new BufferedReader(new InputStreamReader(is));
			while(process.isAlive())
			{
				String line = reader.readLine();
				//System.out.println(line);
			}


			Files.deleteIfExists(Paths.get("reports/" + row.get(0) + ".pdf"));
			Files.move(Paths.get("reports/template/out.pdf"), Paths.get("reports/" + row.get(0) + ".pdf"));
		}






    }

	private String latexEscape(String text) {
    	text = text.trim();
		text = text.replaceAll("\n", "\\\\newline");
		text = text.replaceAll("&", "\\\\&");
		return text;
	}
}
