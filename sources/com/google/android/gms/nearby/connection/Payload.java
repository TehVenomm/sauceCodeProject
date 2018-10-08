package com.google.android.gms.nearby.connection;

import android.os.ParcelFileDescriptor;
import android.os.ParcelFileDescriptor.AutoCloseInputStream;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.DriveFile;
import java.io.FileNotFoundException;
import java.io.InputStream;
import java.util.UUID;

public class Payload {
    private final long id;
    @Type
    private final int type;
    @Nullable
    private final byte[] zzjao;
    @Nullable
    private final File zzjap;
    @Nullable
    private final Stream zzjaq;

    public static class File {
        private final long zzhwh;
        @Nullable
        private final java.io.File zzjar;
        private final ParcelFileDescriptor zzjas;

        private File(@Nullable java.io.File file, ParcelFileDescriptor parcelFileDescriptor, long j) {
            this.zzjar = file;
            this.zzjas = parcelFileDescriptor;
            this.zzhwh = j;
        }

        public static File zza(java.io.File file, long j) throws FileNotFoundException {
            return new File((java.io.File) zzbp.zzb((Object) file, (Object) "Cannot create Payload.File from null java.io.File."), ParcelFileDescriptor.open(file, DriveFile.MODE_READ_ONLY), j);
        }

        public static File zzb(ParcelFileDescriptor parcelFileDescriptor) {
            return new File(null, (ParcelFileDescriptor) zzbp.zzb((Object) parcelFileDescriptor, (Object) "Cannot create Payload.File from null ParcelFileDescriptor."), parcelFileDescriptor.getStatSize());
        }

        @Nullable
        public java.io.File asJavaFile() {
            return this.zzjar;
        }

        @NonNull
        public ParcelFileDescriptor asParcelFileDescriptor() {
            return this.zzjas;
        }

        public long getSize() {
            return this.zzhwh;
        }
    }

    public static class Stream {
        @Nullable
        private final ParcelFileDescriptor zzjas;
        @Nullable
        private InputStream zzjat;

        private Stream(@Nullable ParcelFileDescriptor parcelFileDescriptor, @Nullable InputStream inputStream) {
            this.zzjas = parcelFileDescriptor;
            this.zzjat = inputStream;
        }

        public static Stream zzc(ParcelFileDescriptor parcelFileDescriptor) {
            zzbp.zzb((Object) parcelFileDescriptor, (Object) "Cannot create Payload.Stream from null ParcelFileDescriptor.");
            return new Stream(parcelFileDescriptor, null);
        }

        public static Stream zzi(InputStream inputStream) {
            zzbp.zzb((Object) inputStream, (Object) "Cannot create Payload.Stream from null InputStream.");
            return new Stream(null, inputStream);
        }

        @NonNull
        public InputStream asInputStream() {
            if (this.zzjat == null) {
                this.zzjat = new AutoCloseInputStream(this.zzjas);
            }
            return this.zzjat;
        }

        @Nullable
        public ParcelFileDescriptor asParcelFileDescriptor() {
            return this.zzjas;
        }
    }

    public @interface Type {
        public static final int BYTES = 1;
        public static final int FILE = 2;
        public static final int STREAM = 3;
    }

    private Payload(long j, int i, @Nullable byte[] bArr, @Nullable File file, @Nullable Stream stream) {
        this.id = j;
        this.type = i;
        this.zzjao = bArr;
        this.zzjap = file;
        this.zzjaq = stream;
    }

    public static Payload fromBytes(byte[] bArr) {
        zzbp.zzb((Object) bArr, (Object) "Cannot create a Payload from null bytes.");
        return zza(bArr, UUID.randomUUID().getLeastSignificantBits());
    }

    public static Payload fromFile(ParcelFileDescriptor parcelFileDescriptor) {
        return zza(File.zzb(parcelFileDescriptor), UUID.randomUUID().getLeastSignificantBits());
    }

    public static Payload fromFile(java.io.File file) throws FileNotFoundException {
        return zza(File.zza(file, file.length()), UUID.randomUUID().getLeastSignificantBits());
    }

    public static Payload fromStream(ParcelFileDescriptor parcelFileDescriptor) {
        return zza(Stream.zzc(parcelFileDescriptor), UUID.randomUUID().getLeastSignificantBits());
    }

    public static Payload fromStream(InputStream inputStream) {
        return zza(Stream.zzi(inputStream), UUID.randomUUID().getLeastSignificantBits());
    }

    public static Payload zza(File file, long j) {
        return new Payload(j, 2, null, file, null);
    }

    public static Payload zza(Stream stream, long j) {
        return new Payload(j, 3, null, null, stream);
    }

    public static Payload zza(byte[] bArr, long j) {
        return new Payload(j, 1, bArr, null, null);
    }

    @Nullable
    public byte[] asBytes() {
        return this.zzjao;
    }

    @Nullable
    public File asFile() {
        return this.zzjap;
    }

    @Nullable
    public Stream asStream() {
        return this.zzjaq;
    }

    public long getId() {
        return this.id;
    }

    @Type
    public int getType() {
        return this.type;
    }
}
