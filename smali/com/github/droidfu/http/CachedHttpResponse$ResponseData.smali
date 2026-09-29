.class public final Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;
.super Ljava/lang/Object;
.source "CachedHttpResponse.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/github/droidfu/http/CachedHttpResponse;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "ResponseData"
.end annotation


# instance fields
.field private responseBody:[B

.field private statusCode:I


# direct methods
.method public constructor <init>(I[B)V
    .locals 0

    .line 19
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 20
    iput p1, p0, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->statusCode:I

    .line 21
    iput-object p2, p0, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->responseBody:[B

    return-void
.end method

.method static synthetic access$0(Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;)[B
    .locals 0

    .line 25
    iget-object p0, p0, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->responseBody:[B

    return-object p0
.end method

.method static synthetic access$1(Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;)I
    .locals 0

    .line 24
    iget p0, p0, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->statusCode:I

    return p0
.end method


# virtual methods
.method public getResponseBody()[B
    .locals 1

    .line 32
    iget-object v0, p0, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->responseBody:[B

    return-object v0
.end method

.method public getStatusCode()I
    .locals 1

    .line 28
    iget v0, p0, Lcom/github/droidfu/http/CachedHttpResponse$ResponseData;->statusCode:I

    return v0
.end method
