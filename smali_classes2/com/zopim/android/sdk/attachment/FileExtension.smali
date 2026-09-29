.class public final enum Lcom/zopim/android/sdk/attachment/FileExtension;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/attachment/FileExtension;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/zopim/android/sdk/attachment/FileExtension;

.field public static final enum JPEG:Lcom/zopim/android/sdk/attachment/FileExtension;

.field public static final enum JPG:Lcom/zopim/android/sdk/attachment/FileExtension;

.field public static final enum PDF:Lcom/zopim/android/sdk/attachment/FileExtension;

.field public static final enum PNG:Lcom/zopim/android/sdk/attachment/FileExtension;

.field public static final enum TXT:Lcom/zopim/android/sdk/attachment/FileExtension;

.field public static final enum UNKNOWN:Lcom/zopim/android/sdk/attachment/FileExtension;


# instance fields
.field final extension:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 9

    new-instance v0, Lcom/zopim/android/sdk/attachment/FileExtension;

    const-string v1, "JPG"

    const-string v2, "jpg"

    const/4 v3, 0x0

    invoke-direct {v0, v1, v3, v2}, Lcom/zopim/android/sdk/attachment/FileExtension;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->JPG:Lcom/zopim/android/sdk/attachment/FileExtension;

    new-instance v0, Lcom/zopim/android/sdk/attachment/FileExtension;

    const-string v1, "JPEG"

    const-string v2, "jpeg"

    const/4 v4, 0x1

    invoke-direct {v0, v1, v4, v2}, Lcom/zopim/android/sdk/attachment/FileExtension;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->JPEG:Lcom/zopim/android/sdk/attachment/FileExtension;

    new-instance v0, Lcom/zopim/android/sdk/attachment/FileExtension;

    const-string v1, "PNG"

    const-string v2, "png"

    const/4 v5, 0x2

    invoke-direct {v0, v1, v5, v2}, Lcom/zopim/android/sdk/attachment/FileExtension;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->PNG:Lcom/zopim/android/sdk/attachment/FileExtension;

    new-instance v0, Lcom/zopim/android/sdk/attachment/FileExtension;

    const-string v1, "PDF"

    const-string v2, "pdf"

    const/4 v6, 0x3

    invoke-direct {v0, v1, v6, v2}, Lcom/zopim/android/sdk/attachment/FileExtension;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->PDF:Lcom/zopim/android/sdk/attachment/FileExtension;

    new-instance v0, Lcom/zopim/android/sdk/attachment/FileExtension;

    const-string v1, "TXT"

    const-string v2, "txt"

    const/4 v7, 0x4

    invoke-direct {v0, v1, v7, v2}, Lcom/zopim/android/sdk/attachment/FileExtension;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->TXT:Lcom/zopim/android/sdk/attachment/FileExtension;

    new-instance v0, Lcom/zopim/android/sdk/attachment/FileExtension;

    const-string v1, "UNKNOWN"

    const-string v2, "unknown"

    const/4 v8, 0x5

    invoke-direct {v0, v1, v8, v2}, Lcom/zopim/android/sdk/attachment/FileExtension;-><init>(Ljava/lang/String;ILjava/lang/String;)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->UNKNOWN:Lcom/zopim/android/sdk/attachment/FileExtension;

    const/4 v0, 0x6

    new-array v0, v0, [Lcom/zopim/android/sdk/attachment/FileExtension;

    sget-object v1, Lcom/zopim/android/sdk/attachment/FileExtension;->JPG:Lcom/zopim/android/sdk/attachment/FileExtension;

    aput-object v1, v0, v3

    sget-object v1, Lcom/zopim/android/sdk/attachment/FileExtension;->JPEG:Lcom/zopim/android/sdk/attachment/FileExtension;

    aput-object v1, v0, v4

    sget-object v1, Lcom/zopim/android/sdk/attachment/FileExtension;->PNG:Lcom/zopim/android/sdk/attachment/FileExtension;

    aput-object v1, v0, v5

    sget-object v1, Lcom/zopim/android/sdk/attachment/FileExtension;->PDF:Lcom/zopim/android/sdk/attachment/FileExtension;

    aput-object v1, v0, v6

    sget-object v1, Lcom/zopim/android/sdk/attachment/FileExtension;->TXT:Lcom/zopim/android/sdk/attachment/FileExtension;

    aput-object v1, v0, v7

    sget-object v1, Lcom/zopim/android/sdk/attachment/FileExtension;->UNKNOWN:Lcom/zopim/android/sdk/attachment/FileExtension;

    aput-object v1, v0, v8

    sput-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->$VALUES:[Lcom/zopim/android/sdk/attachment/FileExtension;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;ILjava/lang/String;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    iput-object p3, p0, Lcom/zopim/android/sdk/attachment/FileExtension;->extension:Ljava/lang/String;

    return-void
.end method

.method public static getExtension(Ljava/io/File;)Lcom/zopim/android/sdk/attachment/FileExtension;
    .locals 1

    if-eqz p0, :cond_1

    invoke-virtual {p0}, Ljava/io/File;->getPath()Ljava/lang/String;

    move-result-object v0

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    invoke-virtual {p0}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/zopim/android/sdk/attachment/UriToFileUtil;->getExtension(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/zopim/android/sdk/attachment/FileExtension;->valueOfExtension(Ljava/lang/String;)Lcom/zopim/android/sdk/attachment/FileExtension;

    move-result-object p0

    return-object p0

    :cond_1
    :goto_0
    sget-object p0, Lcom/zopim/android/sdk/attachment/FileExtension;->UNKNOWN:Lcom/zopim/android/sdk/attachment/FileExtension;

    return-object p0
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/attachment/FileExtension;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/attachment/FileExtension;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/attachment/FileExtension;

    return-object p0
.end method

.method public static valueOfExtension(Ljava/lang/String;)Lcom/zopim/android/sdk/attachment/FileExtension;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->JPEG:Lcom/zopim/android/sdk/attachment/FileExtension;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/attachment/FileExtension;->getValue()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0, p0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    sget-object p0, Lcom/zopim/android/sdk/attachment/FileExtension;->JPEG:Lcom/zopim/android/sdk/attachment/FileExtension;

    return-object p0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->JPG:Lcom/zopim/android/sdk/attachment/FileExtension;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/attachment/FileExtension;->getValue()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0, p0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    sget-object p0, Lcom/zopim/android/sdk/attachment/FileExtension;->JPG:Lcom/zopim/android/sdk/attachment/FileExtension;

    return-object p0

    :cond_1
    sget-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->PNG:Lcom/zopim/android/sdk/attachment/FileExtension;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/attachment/FileExtension;->getValue()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0, p0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    sget-object p0, Lcom/zopim/android/sdk/attachment/FileExtension;->PNG:Lcom/zopim/android/sdk/attachment/FileExtension;

    return-object p0

    :cond_2
    sget-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->PDF:Lcom/zopim/android/sdk/attachment/FileExtension;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/attachment/FileExtension;->getValue()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0, p0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_3

    sget-object p0, Lcom/zopim/android/sdk/attachment/FileExtension;->PDF:Lcom/zopim/android/sdk/attachment/FileExtension;

    return-object p0

    :cond_3
    sget-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->TXT:Lcom/zopim/android/sdk/attachment/FileExtension;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/attachment/FileExtension;->getValue()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0, p0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result p0

    if-eqz p0, :cond_4

    sget-object p0, Lcom/zopim/android/sdk/attachment/FileExtension;->TXT:Lcom/zopim/android/sdk/attachment/FileExtension;

    return-object p0

    :cond_4
    sget-object p0, Lcom/zopim/android/sdk/attachment/FileExtension;->UNKNOWN:Lcom/zopim/android/sdk/attachment/FileExtension;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/attachment/FileExtension;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/attachment/FileExtension;->$VALUES:[Lcom/zopim/android/sdk/attachment/FileExtension;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/attachment/FileExtension;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/attachment/FileExtension;

    return-object v0
.end method


# virtual methods
.method public getValue()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/attachment/FileExtension;->extension:Ljava/lang/String;

    return-object v0
.end method
