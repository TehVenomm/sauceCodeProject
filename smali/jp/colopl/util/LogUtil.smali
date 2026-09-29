.class public Ljp/colopl/util/LogUtil;
.super Ljava/lang/Object;
.source "LogUtil.java"


# static fields
.field private static final TEMPORARY_LOG_FILENAME_FORMAT:Ljava/lang/String; = "log.%s.txt"

.field private static file:Ljava/io/File; = null

.field private static forceOutput:Z = false

.field private static logLevel:I = 0x2

.field private static writer:Ljava/io/BufferedWriter;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static d(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x3

    .line 51
    invoke-static {v0, p0, p1}, Ljp/colopl/util/LogUtil;->logToFile(ILjava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static e(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x6

    .line 63
    invoke-static {v0, p0, p1}, Ljp/colopl/util/LogUtil;->logToFile(ILjava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static flush()V
    .locals 1

    .line 89
    sget-boolean v0, Ljp/colopl/util/LogUtil;->forceOutput:Z

    if-nez v0, :cond_0

    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    if-nez v0, :cond_0

    return-void

    .line 92
    :cond_0
    invoke-static {}, Ljp/colopl/util/LogUtil;->getWriter()Ljava/io/BufferedWriter;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 95
    :try_start_0
    invoke-virtual {v0}, Ljava/io/BufferedWriter;->flush()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 97
    invoke-virtual {v0}, Ljava/io/IOException;->printStackTrace()V

    :cond_1
    :goto_0
    return-void
.end method

.method private static formatLogText(ILjava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 3

    const/4 v0, 0x7

    if-ne p0, v0, :cond_0

    const-string p0, "ASSERT"

    goto :goto_0

    :cond_0
    const/4 v0, 0x6

    if-ne p0, v0, :cond_1

    const-string p0, "ERROR"

    goto :goto_0

    :cond_1
    const/4 v0, 0x5

    if-ne p0, v0, :cond_2

    const-string p0, "WARN"

    goto :goto_0

    :cond_2
    const/4 v0, 0x4

    if-ne p0, v0, :cond_3

    const-string p0, "INFO"

    goto :goto_0

    :cond_3
    const/4 v0, 0x3

    if-ne p0, v0, :cond_4

    const-string p0, "DEBUG"

    goto :goto_0

    :cond_4
    const-string p0, "VERBOSE"

    .line 133
    :goto_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "["

    .line 134
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "]["

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "]["

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "yyyy/MM/dd kk:mm:ss"

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    invoke-static {p0, v1, v2}, Landroid/text/format/DateFormat;->format(Ljava/lang/CharSequence;J)Ljava/lang/CharSequence;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/CharSequence;)Ljava/lang/StringBuilder;

    const-string p0, "] "

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 135
    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 136
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static getLogFile(Landroid/content/Context;)Ljava/io/File;
    .locals 6

    .line 34
    new-instance v0, Ljava/io/File;

    invoke-static {}, Landroid/os/Environment;->getExternalStorageDirectory()Ljava/io/File;

    move-result-object v1

    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, v1, p0}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 35
    invoke-virtual {v0}, Ljava/io/File;->exists()Z

    move-result p0

    if-nez p0, :cond_0

    .line 36
    invoke-virtual {v0}, Ljava/io/File;->mkdir()Z

    :cond_0
    const-string p0, "log.%s.txt"

    const/4 v1, 0x1

    .line 38
    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    const-string v3, "yyyyMMdd"

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v4

    invoke-static {v3, v4, v5}, Landroid/text/format/DateFormat;->format(Ljava/lang/CharSequence;J)Ljava/lang/CharSequence;

    move-result-object v3

    aput-object v3, v1, v2

    invoke-static {p0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p0

    .line 39
    new-instance v1, Ljava/io/File;

    invoke-direct {v1, v0, p0}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    return-object v1
.end method

.method private static getWriter()Ljava/io/BufferedWriter;
    .locals 4

    .line 103
    sget-object v0, Ljp/colopl/util/LogUtil;->writer:Ljava/io/BufferedWriter;

    if-nez v0, :cond_0

    .line 105
    :try_start_0
    new-instance v0, Ljava/io/BufferedWriter;

    new-instance v1, Ljava/io/FileWriter;

    sget-object v2, Ljp/colopl/util/LogUtil;->file:Ljava/io/File;

    const/4 v3, 0x1

    invoke-direct {v1, v2, v3}, Ljava/io/FileWriter;-><init>(Ljava/io/File;Z)V

    invoke-direct {v0, v1}, Ljava/io/BufferedWriter;-><init>(Ljava/io/Writer;)V

    sput-object v0, Ljp/colopl/util/LogUtil;->writer:Ljava/io/BufferedWriter;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 107
    invoke-virtual {v0}, Ljava/lang/Exception;->printStackTrace()V

    .line 110
    :cond_0
    :goto_0
    sget-object v0, Ljp/colopl/util/LogUtil;->writer:Ljava/io/BufferedWriter;

    return-object v0
.end method

.method public static i(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x4

    .line 55
    invoke-static {v0, p0, p1}, Ljp/colopl/util/LogUtil;->logToFile(ILjava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static logToFile(ILjava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 67
    sget-boolean v0, Ljp/colopl/util/LogUtil;->forceOutput:Z

    if-nez v0, :cond_0

    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    if-nez v0, :cond_0

    return-void

    .line 70
    :cond_0
    sget v0, Ljp/colopl/util/LogUtil;->logLevel:I

    if-ge p0, v0, :cond_1

    return-void

    .line 73
    :cond_1
    invoke-static {p0, p1, p2}, Landroid/util/Log;->println(ILjava/lang/String;Ljava/lang/String;)I

    .line 75
    :try_start_0
    invoke-static {}, Ljp/colopl/util/LogUtil;->getWriter()Ljava/io/BufferedWriter;

    move-result-object v0

    if-nez v0, :cond_2

    return-void

    .line 79
    :cond_2
    invoke-static {p0, p1, p2}, Ljp/colopl/util/LogUtil;->formatLogText(ILjava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    .line 80
    invoke-virtual {v0, p0}, Ljava/io/BufferedWriter;->write(Ljava/lang/String;)V

    .line 81
    invoke-virtual {v0}, Ljava/io/BufferedWriter;->newLine()V

    .line 82
    invoke-virtual {v0}, Ljava/io/BufferedWriter;->flush()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p0

    .line 84
    invoke-virtual {p0}, Ljava/lang/Exception;->printStackTrace()V

    :goto_0
    return-void
.end method

.method public static setLevel(I)V
    .locals 0

    .line 43
    sput p0, Ljp/colopl/util/LogUtil;->logLevel:I

    return-void
.end method

.method public static setup(Landroid/content/Context;)V
    .locals 1

    .line 27
    sget-boolean v0, Ljp/colopl/util/LogUtil;->forceOutput:Z

    if-nez v0, :cond_0

    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    if-nez v0, :cond_0

    return-void

    .line 30
    :cond_0
    invoke-static {p0}, Ljp/colopl/util/LogUtil;->getLogFile(Landroid/content/Context;)Ljava/io/File;

    move-result-object p0

    sput-object p0, Ljp/colopl/util/LogUtil;->file:Ljava/io/File;

    return-void
.end method

.method public static showToast(Landroid/content/Context;Ljava/lang/String;I)V
    .locals 1

    .line 140
    sget-boolean v0, Ljp/colopl/util/LogUtil;->forceOutput:Z

    if-nez v0, :cond_0

    sget-boolean v0, Ljp/colopl/config/Config;->debuggable:Z

    if-nez v0, :cond_0

    return-void

    .line 143
    :cond_0
    invoke-static {p0, p1, p2}, Landroid/widget/Toast;->makeText(Landroid/content/Context;Ljava/lang/CharSequence;I)Landroid/widget/Toast;

    move-result-object p0

    invoke-virtual {p0}, Landroid/widget/Toast;->show()V

    return-void
.end method

.method public static v(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x2

    .line 47
    invoke-static {v0, p0, p1}, Ljp/colopl/util/LogUtil;->logToFile(ILjava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public static w(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x5

    .line 59
    invoke-static {v0, p0, p1}, Ljp/colopl/util/LogUtil;->logToFile(ILjava/lang/String;Ljava/lang/String;)V

    return-void
.end method
