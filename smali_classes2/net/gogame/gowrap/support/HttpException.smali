.class public Lnet/gogame/gowrap/support/HttpException;
.super Ljava/lang/Exception;
.source "HttpException.java"


# instance fields
.field private final statusCode:I

.field private final statusMessage:Ljava/lang/String;


# direct methods
.method public constructor <init>(ILjava/lang/String;)V
    .locals 5

    .line 11
    sget-object v0, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    const-string v1, "%d %s"

    const/4 v2, 0x2

    new-array v2, v2, [Ljava/lang/Object;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    const/4 v4, 0x0

    aput-object v3, v2, v4

    const/4 v3, 0x1

    aput-object p2, v2, v3

    invoke-static {v0, v1, v2}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v0}, Ljava/lang/Exception;-><init>(Ljava/lang/String;)V

    .line 13
    iput p1, p0, Lnet/gogame/gowrap/support/HttpException;->statusCode:I

    .line 14
    iput-object p2, p0, Lnet/gogame/gowrap/support/HttpException;->statusMessage:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getStatusCode()I
    .locals 1

    .line 18
    iget v0, p0, Lnet/gogame/gowrap/support/HttpException;->statusCode:I

    return v0
.end method

.method public getStatusMessage()Ljava/lang/String;
    .locals 1

    .line 22
    iget-object v0, p0, Lnet/gogame/gowrap/support/HttpException;->statusMessage:Ljava/lang/String;

    return-object v0
.end method
