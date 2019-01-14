import com.avans.tentamenmanager.GoogleSheetOrganizer;
import com.avans.tentamenmanager.TestManager;
import freemarker.template.TemplateException;
import org.junit.Test;

import java.io.IOException;
import java.security.GeneralSecurityException;

public class TestReport {
	@Test
	public void testReport() throws GeneralSecurityException, IOException, TemplateException {
		TestManager testManager = new TestManager();
		GoogleSheetOrganizer googleSheet = new GoogleSheetOrganizer(testManager);
		googleSheet.buildReports();

	}


}