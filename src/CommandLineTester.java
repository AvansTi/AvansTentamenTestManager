import com.avans.tentamenmanager.TestManager;

public class CommandLineTester {
	public static void main(String[] args)
	{
		TestManager testManager = new TestManager();
		testManager.setPath("C:\\Users\\johan\\Desktop\\Avans\\Kwartalen\\Voltijd TI\\1.2 Voltijd\\2018-2019\\OGP1\\Tentamen\\work2");

		testManager.runAllTests();
	}
}
