package bolts;

import java.io.PrintStream;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import org.apache.commons.lang3.StringUtils;

public class AggregateException extends Exception {
    private static final String DEFAULT_MESSAGE = "There were multiple errors.";
    private static final long serialVersionUID = 1;
    private List<Throwable> innerThrowables;

    public AggregateException(String str, List<? extends Throwable> list) {
        super(str, (list == null || list.size() <= 0) ? null : (Throwable) list.get(0));
        this.innerThrowables = Collections.unmodifiableList(list);
    }

    public AggregateException(String str, Throwable[] thArr) {
        this(str, Arrays.asList(thArr));
    }

    public AggregateException(List<? extends Throwable> list) {
        this(DEFAULT_MESSAGE, list);
    }

    @Deprecated
    public Throwable[] getCauses() {
        return (Throwable[]) this.innerThrowables.toArray(new Throwable[this.innerThrowables.size()]);
    }

    @Deprecated
    public List<Exception> getErrors() {
        ArrayList arrayList = new ArrayList();
        if (this.innerThrowables != null) {
            for (Throwable th : this.innerThrowables) {
                if (th instanceof Exception) {
                    arrayList.add((Exception) th);
                } else {
                    arrayList.add(new Exception(th));
                }
            }
        }
        return arrayList;
    }

    public List<Throwable> getInnerThrowables() {
        return this.innerThrowables;
    }

    public void printStackTrace(PrintStream printStream) {
        super.printStackTrace(printStream);
        int i = -1;
        for (Throwable th : this.innerThrowables) {
            printStream.append(StringUtils.f1199LF);
            printStream.append("  Inner throwable #");
            i++;
            printStream.append(Integer.toString(i));
            printStream.append(": ");
            th.printStackTrace(printStream);
            printStream.append(StringUtils.f1199LF);
        }
    }

    public void printStackTrace(PrintWriter printWriter) {
        super.printStackTrace(printWriter);
        int i = -1;
        for (Throwable th : this.innerThrowables) {
            printWriter.append(StringUtils.f1199LF);
            printWriter.append("  Inner throwable #");
            i++;
            printWriter.append(Integer.toString(i));
            printWriter.append(": ");
            th.printStackTrace(printWriter);
            printWriter.append(StringUtils.f1199LF);
        }
    }
}
