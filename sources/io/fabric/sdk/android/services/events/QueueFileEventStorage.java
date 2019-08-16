package p017io.fabric.sdk.android.services.events;

import android.content.Context;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.common.QueueFile;

/* renamed from: io.fabric.sdk.android.services.events.QueueFileEventStorage */
public class QueueFileEventStorage implements EventsStorage {
    private final Context context;
    private QueueFile queueFile = new QueueFile(this.workingFile);
    private File targetDirectory;
    private final String targetDirectoryName;
    private final File workingDirectory;
    private final File workingFile;

    public QueueFileEventStorage(Context context2, File file, String str, String str2) throws IOException {
        this.context = context2;
        this.workingDirectory = file;
        this.targetDirectoryName = str2;
        this.workingFile = new File(this.workingDirectory, str);
        createTargetDirectory();
    }

    private void createTargetDirectory() {
        this.targetDirectory = new File(this.workingDirectory, this.targetDirectoryName);
        if (!this.targetDirectory.exists()) {
            this.targetDirectory.mkdirs();
        }
    }

    private void move(File file, File file2) throws IOException {
        OutputStream outputStream;
        FileInputStream fileInputStream;
        OutputStream outputStream2 = null;
        try {
            fileInputStream = new FileInputStream(file);
            try {
                outputStream2 = getMoveOutputStream(file2);
                CommonUtils.copyStream(fileInputStream, outputStream2, new byte[1024]);
                CommonUtils.closeOrLog(fileInputStream, "Failed to close file input stream");
                CommonUtils.closeOrLog(outputStream2, "Failed to close output stream");
                file.delete();
            } catch (Throwable th) {
                th = th;
                outputStream = outputStream2;
                CommonUtils.closeOrLog(fileInputStream, "Failed to close file input stream");
                CommonUtils.closeOrLog(outputStream, "Failed to close output stream");
                file.delete();
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
            fileInputStream = null;
            outputStream = null;
            CommonUtils.closeOrLog(fileInputStream, "Failed to close file input stream");
            CommonUtils.closeOrLog(outputStream, "Failed to close output stream");
            file.delete();
            throw th;
        }
    }

    public void add(byte[] bArr) throws IOException {
        this.queueFile.add(bArr);
    }

    public boolean canWorkingFileStore(int i, int i2) {
        return this.queueFile.hasSpaceFor(i, i2);
    }

    public void deleteFilesInRollOverDirectory(List<File> list) {
        for (File file : list) {
            CommonUtils.logControlled(this.context, String.format("deleting sent analytics file %s", new Object[]{file.getName()}));
            file.delete();
        }
    }

    public void deleteWorkingFile() {
        try {
            this.queueFile.close();
        } catch (IOException e) {
        }
        this.workingFile.delete();
    }

    public List<File> getAllFilesInRollOverDirectory() {
        return Arrays.asList(this.targetDirectory.listFiles());
    }

    public List<File> getBatchOfFilesToSend(int i) {
        ArrayList arrayList = new ArrayList();
        for (File add : this.targetDirectory.listFiles()) {
            arrayList.add(add);
            if (arrayList.size() >= i) {
                break;
            }
        }
        return arrayList;
    }

    public OutputStream getMoveOutputStream(File file) throws IOException {
        return new FileOutputStream(file);
    }

    public File getRollOverDirectory() {
        return this.targetDirectory;
    }

    public File getWorkingDirectory() {
        return this.workingDirectory;
    }

    public int getWorkingFileUsedSizeInBytes() {
        return this.queueFile.usedBytes();
    }

    public boolean isWorkingFileEmpty() {
        return this.queueFile.isEmpty();
    }

    public void rollOver(String str) throws IOException {
        this.queueFile.close();
        move(this.workingFile, new File(this.targetDirectory, str));
        this.queueFile = new QueueFile(this.workingFile);
    }
}
