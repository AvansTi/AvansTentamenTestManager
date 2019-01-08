import com.avans.tentamenmanager.GoogleSheetOrganizer;
import com.avans.tentamenmanager.TestManager;
import org.junit.Test;

import java.io.IOException;
import java.security.GeneralSecurityException;

import static org.junit.Assert.assertEquals;

public class TestGoogleSheet {


    @Test
    public void columnStuff() throws GeneralSecurityException, IOException {
        GoogleSheetOrganizer gs = new GoogleSheetOrganizer(new TestManager());

        assertEquals(gs.getColIndex("A"), 1);
        assertEquals(gs.getColIndex("Z"), 26);
        assertEquals(gs.getColIndex("AA"), 27);
        assertEquals(gs.getColIndex("AB"), 28);
        assertEquals(gs.getColIndex("AZ"), 52);


        assertEquals(gs.getColName(1), "A");
        assertEquals(gs.getColName(2), "B");
        assertEquals(gs.getColName(26), "Z");
        assertEquals(gs.getColName(27), "AA");
        assertEquals(gs.getColName(28), "AB");
        assertEquals(gs.getColName(52), "AZ");


    }
}
