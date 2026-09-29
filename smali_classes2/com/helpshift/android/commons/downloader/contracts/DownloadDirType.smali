.class public final enum Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;
.super Ljava/lang/Enum;
.source "DownloadDirType.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

.field public static final enum EXTERNAL_ONLY:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

.field public static final enum EXTERNAL_OR_INTERNAL:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

.field public static final enum INTERNAL_ONLY:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 8
    new-instance v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    const-string v1, "INTERNAL_ONLY"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->INTERNAL_ONLY:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    .line 14
    new-instance v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    const-string v1, "EXTERNAL_ONLY"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->EXTERNAL_ONLY:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    .line 23
    new-instance v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    const-string v1, "EXTERNAL_OR_INTERNAL"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->EXTERNAL_OR_INTERNAL:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    const/4 v0, 0x3

    .line 3
    new-array v0, v0, [Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    sget-object v1, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->INTERNAL_ONLY:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->EXTERNAL_ONLY:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->EXTERNAL_OR_INTERNAL:Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    aput-object v1, v0, v4

    sput-object v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->$VALUES:[Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 3
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;
    .locals 1

    .line 3
    const-class v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;
    .locals 1

    .line 3
    sget-object v0, Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->$VALUES:[Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    invoke-virtual {v0}, [Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/android/commons/downloader/contracts/DownloadDirType;

    return-object v0
.end method
