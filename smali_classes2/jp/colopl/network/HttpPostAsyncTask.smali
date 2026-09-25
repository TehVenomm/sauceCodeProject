.class public Ljp/colopl/network/HttpPostAsyncTask;
.super Lcom/github/droidfu/concurrent/BetterAsyncTask;
.source "HttpPostAsyncTask.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/github/droidfu/concurrent/BetterAsyncTask<",
        "Ljava/lang/Void;",
        "Ljava/lang/Void;",
        "Ljava/lang/String;",
        ">;"
    }
.end annotation


# instance fields
.field private cookies:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private listener:Ljp/colopl/network/HttpRequestListener;

.field private postData:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;"
        }
    .end annotation
.end field

.field private tag:I

.field private url:Ljava/lang/String;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;)V"
        }
    .end annotation

    .line 36
    invoke-direct {p0, p1}, Lcom/github/droidfu/concurrent/BetterAsyncTask;-><init>(Landroid/content/Context;)V

    .line 37
    iput-object p2, p0, Ljp/colopl/network/HttpPostAsyncTask;->url:Ljava/lang/String;

    .line 38
    iput-object p3, p0, Ljp/colopl/network/HttpPostAsyncTask;->postData:Ljava/util/List;

    .line 39
    new-instance p2, Ljp/colopl/config/Config;

    invoke-direct {p2, p1}, Ljp/colopl/config/Config;-><init>(Landroid/content/Context;)V

    .line 40
    invoke-static {p2}, Ljp/colopl/util/HTTP;->createCookies(Ljp/colopl/config/Config;)Ljava/util/List;

    move-result-object p1

    iput-object p1, p0, Ljp/colopl/network/HttpPostAsyncTask;->cookies:Ljava/util/List;

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/util/List;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 44
    invoke-direct {p0, p1}, Lcom/github/droidfu/concurrent/BetterAsyncTask;-><init>(Landroid/content/Context;)V

    .line 45
    iput-object p2, p0, Ljp/colopl/network/HttpPostAsyncTask;->url:Ljava/lang/String;

    .line 46
    iput-object p3, p0, Ljp/colopl/network/HttpPostAsyncTask;->postData:Ljava/util/List;

    .line 47
    iput-object p4, p0, Ljp/colopl/network/HttpPostAsyncTask;->cookies:Ljava/util/List;

    return-void
.end method


# virtual methods
.method protected bridge synthetic after(Landroid/content/Context;Ljava/lang/Object;)V
    .locals 0

    .line 15
    check-cast p2, Ljava/lang/String;

    invoke-virtual {p0, p1, p2}, Ljp/colopl/network/HttpPostAsyncTask;->after(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method

.method protected after(Landroid/content/Context;Ljava/lang/String;)V
    .locals 0

    .line 61
    iget-object p1, p0, Ljp/colopl/network/HttpPostAsyncTask;->listener:Ljp/colopl/network/HttpRequestListener;

    if-eqz p1, :cond_0

    .line 62
    iget-object p1, p0, Ljp/colopl/network/HttpPostAsyncTask;->listener:Ljp/colopl/network/HttpRequestListener;

    invoke-interface {p1, p0, p2}, Ljp/colopl/network/HttpRequestListener;->onReceiveResponse(Ljp/colopl/network/HttpPostAsyncTask;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method protected bridge synthetic doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 15
    check-cast p2, [Ljava/lang/Void;

    invoke-virtual {p0, p1, p2}, Ljp/colopl/network/HttpPostAsyncTask;->doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Void;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method protected varargs doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Void;)Ljava/lang/String;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 53
    invoke-super {p0, p1, p2}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Object;)Ljava/lang/Object;

    .line 54
    iget-object p1, p0, Ljp/colopl/network/HttpPostAsyncTask;->url:Ljava/lang/String;

    iget-object p2, p0, Ljp/colopl/network/HttpPostAsyncTask;->postData:Ljava/util/List;

    iget-object v0, p0, Ljp/colopl/network/HttpPostAsyncTask;->cookies:Ljava/util/List;

    invoke-static {p1, p2, v0}, Ljp/colopl/util/HTTP;->post(Ljava/lang/String;Ljava/util/List;Ljava/util/List;)Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_0

    return-object p1

    .line 55
    :cond_0
    new-instance p1, Ljava/lang/Exception;

    invoke-direct {p1}, Ljava/lang/Exception;-><init>()V

    throw p1
.end method

.method public getTag()I
    .locals 1

    .line 28
    iget v0, p0, Ljp/colopl/network/HttpPostAsyncTask;->tag:I

    return v0
.end method

.method protected handleError(Landroid/content/Context;Ljava/lang/Exception;)V
    .locals 0

    .line 69
    iget-object p1, p0, Ljp/colopl/network/HttpPostAsyncTask;->listener:Ljp/colopl/network/HttpRequestListener;

    if-eqz p1, :cond_0

    .line 70
    iget-object p1, p0, Ljp/colopl/network/HttpPostAsyncTask;->listener:Ljp/colopl/network/HttpRequestListener;

    invoke-interface {p1, p0, p2}, Ljp/colopl/network/HttpRequestListener;->onReceiveError(Ljp/colopl/network/HttpPostAsyncTask;Ljava/lang/Exception;)V

    :cond_0
    return-void
.end method

.method public setListener(Ljp/colopl/network/HttpRequestListener;)V
    .locals 0

    .line 24
    iput-object p1, p0, Ljp/colopl/network/HttpPostAsyncTask;->listener:Ljp/colopl/network/HttpRequestListener;

    return-void
.end method

.method public setTag(I)V
    .locals 0

    .line 32
    iput p1, p0, Ljp/colopl/network/HttpPostAsyncTask;->tag:I

    return-void
.end method
