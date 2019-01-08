package com.avans.tentamenmanager;

import com.google.api.client.auth.oauth2.Credential;
import com.google.api.client.extensions.java6.auth.oauth2.AuthorizationCodeInstalledApp;
import com.google.api.client.extensions.jetty.auth.oauth2.LocalServerReceiver;
import com.google.api.client.googleapis.auth.oauth2.GoogleAuthorizationCodeFlow;
import com.google.api.client.googleapis.auth.oauth2.GoogleClientSecrets;
import com.google.api.client.googleapis.javanet.GoogleNetHttpTransport;
import com.google.api.client.http.javanet.NetHttpTransport;
import com.google.api.client.json.JsonFactory;
import com.google.api.client.json.JsonObjectParser;
import com.google.api.client.json.jackson2.JacksonFactory;
import com.google.api.client.util.store.FileDataStoreFactory;
import com.google.api.services.sheets.v4.Sheets;
import com.google.api.services.sheets.v4.SheetsScopes;
import com.google.api.services.sheets.v4.model.*;
import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;
import javafx.scene.control.TextInputDialog;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
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

/*        Sheet testResults = findOrCreateSheet("tentamen tests");
        Sheet overview = findOrCreateSheet("tentamen overview");
        Sheet theory = findOrCreateSheet("tentamen theorie");
        Sheet manualOverride = findOrCreateSheet("tentamen handmatig");*/


        //buildTestResultSheet();
        //buildManualCorrection();
        //buildOverview();
    }

    public void buildManualCorrection() throws IOException {
        String title = "'Tentamen Handmatig'";
        System.out.println("Building test result sheet");
        service.spreadsheets().values().clear(spreadsheetId, title, new ClearValuesRequest()).execute();

        Sheet sheet = findSheet("Tentamen Handmatig");


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
            row.add(s.getStudentId());

            row.add("=VLOOKUP($A"+(values.size()+1)+", Studentenlijst!$A$4:$H$999, 5, false)");
            row.add("=VLOOKUP($A"+(values.size()+1)+", Studentenlijst!$A$4:$H$999, 8, false)");


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
        System.out.println("Updating test result sheet");

        service.spreadsheets().values().append(spreadsheetId, title, new ValueRange().setValues(values)).setValueInputOption("USER_ENTERED").execute();
        System.out.println("Done with test result sheet");

        ArrayList<Request> requests = new ArrayList<>();


        /*
        SortSpec ss = new SortSpec();
        ss.setSortOrder("ASCENDING");
        ss.setDimensionIndex(2); //0 index
        SortRangeRequest srr = new SortRangeRequest();
        srr.setRange(new GridRange().setSheetId(sheet.getProperties().getSheetId()).setStartRowIndex(1));
        srr.setSortSpecs(Arrays.asList(ss));
        Request req = new Request();
        req.setSortRange(srr);
        requests.add(req);*/

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
// mService is a insetance of com.google.api.services.sheets.v4.Sheets
        service.spreadsheets().batchUpdate(spreadsheetId, busReq).execute();




    }

    public String getColName(int col) {
        final String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        String ret = "";
        while(col != 0)
        {
            ret = alphabet.charAt((col-1)%26) + ret;
            col /= 26;
        }

        return ret;
    }
	public int getColIndex(String col) {
		final String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		return 0;
	}


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

        if(service.spreadsheets().values().get(spreadsheetId, studentlistSheetName).execute().getValues().size() == 0)
        {
        	showError("Could not find student list");
        	return;
        }

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

        field.get(1).set(0, "=QUERY(Studentenlijst!A4:H999,\"select A where A > 0 order by H\")");



        for(int i = 1; i < studentCount; i++) {
            field.get(i).set(1, "=VLOOKUP($A"+(i+1)+", Studentenlijst!$A$4:$H$999, 5, false)");
            field.get(i).set(2, "=VLOOKUP($A"+(i+1)+", Studentenlijst!$A$4:$H$999, 8, false)");
            field.get(i).set(3, addNumberCheck("VLOOKUP($A"+(i+1)+", 'Tentamen Tests'!$A$4:$ZZ$999, "+testRowCount+", false)", "NA"));
            field.get(i).set(4, addNumberCheck("VLOOKUP($A"+(i+1)+", 'Tentamen Handmatig'!$A$4:$ZZ$999, "+manualRowCount+", false)", "NA"));
            field.get(i).set(5, addNumberCheck("VLOOKUP($A"+(i+1)+", 'Tentamen Theorie'!$A$4:$ZZ$999, "+theoryCount+", false)", "NA"));
            field.get(i).set(6, "=IF(ISNUMBER(D" + (i+1) + "), D" + (i+1) + ", 0) "+
                            "+ IF(ISNUMBER(E" + (i+1) + "), E" + (i+1) + ", 0) "+
                            "+ IF(ISNUMBER(F" + (i+1) + "), F" + (i+1) + ", 0) "+
                    "");
            field.get(i).set(7, "=ROUND(G" + (i+1) + "/20, 1)");
        }

        service.spreadsheets().values().update(spreadsheetId, overviewName, new ValueRange().setValues(field)).setValueInputOption("USER_ENTERED").execute();

    }

	private void showError(String message) {
    	Alert alert = new Alert(Alert.AlertType.ERROR, message, ButtonType.OK);
    	alert.showAndWait();
	}

	private String addNumberCheck(String query, String ifNaN)
    {
        return "=IF(ISNUMBER(" + query + "), " + query + ", \"" + ifNaN + "\")";
    }




    public void buildTestResultSheet() throws IOException {
        String title = "'Tentamen Tests'";
        System.out.println("Building test result sheet");
        service.spreadsheets().values().clear(spreadsheetId, title, new ClearValuesRequest()).execute();

        final List<List<Object>> values = new ArrayList<>();
        //add header
        {
            List<Object> header = new ArrayList<>();
            header.add("StudentNumber");
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
        System.out.println("Updating test result sheet");

        service.spreadsheets().values().append(spreadsheetId, title, new ValueRange().setValues(values)).setValueInputOption("USER_ENTERED").execute();
        System.out.println("Done with test result sheet");

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

    private String quoteSheetName(String sheetName)
    {
        if(sheetName.contains(" "))
            return "'" + sheetName + "'";
        return sheetName;
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
}
