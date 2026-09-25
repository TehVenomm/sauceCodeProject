.class public Lcom/helpshift/conversation/dto/ImagePickerFile;
.super Ljava/lang/Object;
.source "ImagePickerFile.java"


# instance fields
.field public filePath:Ljava/lang/String;

.field public isFileCompressionAndCopyingDone:Z

.field public final originalFileName:Ljava/lang/String;

.field public final originalFileSize:Ljava/lang/Long;

.field public transientUri:Ljava/lang/Object;


# direct methods
.method public constructor <init>(Ljava/lang/Object;Ljava/lang/String;Ljava/lang/Long;)V
    .locals 0

    .line 39
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 40
    iput-object p1, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->transientUri:Ljava/lang/Object;

    .line 41
    iput-object p2, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->originalFileName:Ljava/lang/String;

    .line 42
    iput-object p3, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->originalFileSize:Ljava/lang/Long;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Long;)V
    .locals 0

    .line 33
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 34
    iput-object p1, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    .line 35
    iput-object p2, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->originalFileName:Ljava/lang/String;

    .line 36
    iput-object p3, p0, Lcom/helpshift/conversation/dto/ImagePickerFile;->originalFileSize:Ljava/lang/Long;

    return-void
.end method
