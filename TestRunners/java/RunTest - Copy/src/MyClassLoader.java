import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLClassLoader;
import java.nio.file.Paths;

public class MyClassLoader extends URLClassLoader {
    public static MyClassLoader instance;


    public MyClassLoader(ClassLoader launcherClassLoader) {
        super(new URL[] { }, launcherClassLoader);
        instance = this;
    }


    public void appendToClassPathForInstrumentation(String url) {
        try {
            this.addURL(Paths.get(url).toUri().toURL());
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }
    }
    public void addURL(URL url)
    {
        super.addURL(url);
    }

}