.class public final enum Lcom/helpshift/downloader/SupportDownloader$StorageDirType;
.super Ljava/lang/Enum;
.source "SupportDownloader.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/downloader/SupportDownloader;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4019
    name = "StorageDirType"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/helpshift/downloader/SupportDownloader$StorageDirType;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

.field public static final enum EXTERNAL_ONLY:Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

.field public static final enum EXTERNAL_OR_INTERNAL:Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

.field public static final enum INTERNAL_ONLY:Lcom/helpshift/downloader/SupportDownloader$StorageDirType;


# direct methods
.method static constructor <clinit>()V
    .locals 5

    .line 17
    new-instance v0, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    const-string v1, "INTERNAL_ONLY"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->INTERNAL_ONLY:Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    .line 23
    new-instance v0, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    const-string v1, "EXTERNAL_ONLY"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->EXTERNAL_ONLY:Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    .line 32
    new-instance v0, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    const-string v1, "EXTERNAL_OR_INTERNAL"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->EXTERNAL_OR_INTERNAL:Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    const/4 v0, 0x3

    .line 12
    new-array v0, v0, [Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    sget-object v1, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->INTERNAL_ONLY:Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    aput-object v1, v0, v2

    sget-object v1, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->EXTERNAL_ONLY:Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    aput-object v1, v0, v3

    sget-object v1, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->EXTERNAL_OR_INTERNAL:Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    aput-object v1, v0, v4

    sput-object v0, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->$VALUES:[Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 12
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/helpshift/downloader/SupportDownloader$StorageDirType;
    .locals 1

    .line 12
    const-class v0, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    return-object p0
.end method

.method public static values()[Lcom/helpshift/downloader/SupportDownloader$StorageDirType;
    .locals 1

    .line 12
    sget-object v0, Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->$VALUES:[Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    invoke-virtual {v0}, [Lcom/helpshift/downloader/SupportDownloader$StorageDirType;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/helpshift/downloader/SupportDownloader$StorageDirType;

    return-object v0
.end method
