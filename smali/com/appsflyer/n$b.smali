.class final enum Lcom/appsflyer/n$b;
.super Ljava/lang/Enum;
.source ""


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/appsflyer/n;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x4018
    name = "b"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/appsflyer/n$b;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic ˊ:[Lcom/appsflyer/n$b;

.field public static final enum ˋ:Lcom/appsflyer/n$b;

.field private static enum ˏ:Lcom/appsflyer/n$b;


# instance fields
.field private ˎ:I


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 70
    new-instance v0, Lcom/appsflyer/n$b;

    const-string v1, "GOOGLE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2, v2}, Lcom/appsflyer/n$b;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/appsflyer/n$b;->ˏ:Lcom/appsflyer/n$b;

    new-instance v0, Lcom/appsflyer/n$b;

    const-string v1, "AMAZON"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3, v3}, Lcom/appsflyer/n$b;-><init>(Ljava/lang/String;II)V

    sput-object v0, Lcom/appsflyer/n$b;->ˋ:Lcom/appsflyer/n$b;

    const/4 v0, 0x2

    .line 69
    new-array v0, v0, [Lcom/appsflyer/n$b;

    sget-object v1, Lcom/appsflyer/n$b;->ˏ:Lcom/appsflyer/n$b;

    aput-object v1, v0, v2

    sget-object v1, Lcom/appsflyer/n$b;->ˋ:Lcom/appsflyer/n$b;

    aput-object v1, v0, v3

    sput-object v0, Lcom/appsflyer/n$b;->ˊ:[Lcom/appsflyer/n$b;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;II)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)V"
        }
    .end annotation

    .line 74
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    .line 75
    iput p3, p0, Lcom/appsflyer/n$b;->ˎ:I

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/appsflyer/n$b;
    .locals 1

    .line 69
    const-class v0, Lcom/appsflyer/n$b;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/appsflyer/n$b;

    return-object p0
.end method

.method public static values()[Lcom/appsflyer/n$b;
    .locals 1

    .line 69
    sget-object v0, Lcom/appsflyer/n$b;->ˊ:[Lcom/appsflyer/n$b;

    invoke-virtual {v0}, [Lcom/appsflyer/n$b;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/appsflyer/n$b;

    return-object v0
.end method


# virtual methods
.method public final toString()Ljava/lang/String;
    .locals 1

    .line 92
    iget v0, p0, Lcom/appsflyer/n$b;->ˎ:I

    invoke-static {v0}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
