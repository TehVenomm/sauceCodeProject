.class public final Lcom/helpshift/support/model/AttachmentFileSize;
.super Ljava/lang/Object;
.source "AttachmentFileSize.java"


# static fields
.field private static final FILE_SIZE_UNIT_B:Ljava/lang/String; = " B"

.field private static final FILE_SIZE_UNIT_KB:Ljava/lang/String; = " KB"

.field private static final FILE_SIZE_UNIT_MB:Ljava/lang/String; = " MB"


# instance fields
.field private fileSize:D

.field private fileSizeUnit:Ljava/lang/String;


# direct methods
.method public constructor <init>(D)V
    .locals 5

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-wide/high16 v0, 0x4090000000000000L    # 1024.0

    cmpg-double v2, p1, v0

    if-gez v2, :cond_0

    .line 19
    iput-wide p1, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSize:D

    const-string p1, " B"

    .line 20
    iput-object p1, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSizeUnit:Ljava/lang/String;

    goto :goto_0

    :cond_0
    const-wide/high16 v2, 0x4130000000000000L    # 1048576.0

    cmpg-double v4, p1, v2

    if-gez v4, :cond_1

    div-double/2addr p1, v0

    .line 23
    iput-wide p1, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSize:D

    const-string p1, " KB"

    .line 24
    iput-object p1, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSizeUnit:Ljava/lang/String;

    goto :goto_0

    :cond_1
    div-double/2addr p1, v2

    .line 27
    iput-wide p1, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSize:D

    const-string p1, " MB"

    .line 28
    iput-object p1, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSizeUnit:Ljava/lang/String;

    :goto_0
    return-void
.end method


# virtual methods
.method public getFormattedFileSize()Ljava/lang/String;
    .locals 7

    .line 34
    iget-object v0, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSizeUnit:Ljava/lang/String;

    const-string v1, " MB"

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-eqz v0, :cond_0

    .line 35
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v4, "%.1f"

    new-array v2, v2, [Ljava/lang/Object;

    iget-wide v5, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSize:D

    invoke-static {v5, v6}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v5

    aput-object v5, v2, v1

    invoke-static {v3, v4, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSizeUnit:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    .line 38
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v4, "%.0f"

    new-array v2, v2, [Ljava/lang/Object;

    iget-wide v5, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSize:D

    invoke-static {v5, v6}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v5

    aput-object v5, v2, v1

    invoke-static {v3, v4, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/support/model/AttachmentFileSize;->fileSizeUnit:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    :goto_0
    return-object v0
.end method
