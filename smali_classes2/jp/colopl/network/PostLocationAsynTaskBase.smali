.class public abstract Ljp/colopl/network/PostLocationAsynTaskBase;
.super Lcom/github/droidfu/concurrent/BetterAsyncTask;
.source "PostLocationAsynTaskBase.java"


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
.field private mConfig:Ljp/colopl/config/Config;

.field private mLocations:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Landroid/location/Location;",
            ">;"
        }
    .end annotation
.end field

.field private mPostUrl:Ljava/lang/String;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/util/HashMap;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Landroid/location/Location;",
            ">;)V"
        }
    .end annotation

    .line 23
    invoke-direct {p0, p1}, Lcom/github/droidfu/concurrent/BetterAsyncTask;-><init>(Landroid/content/Context;)V

    .line 25
    iput-object p2, p0, Ljp/colopl/network/PostLocationAsynTaskBase;->mPostUrl:Ljava/lang/String;

    .line 26
    iput-object p3, p0, Ljp/colopl/network/PostLocationAsynTaskBase;->mLocations:Ljava/util/HashMap;

    .line 27
    new-instance p2, Ljp/colopl/config/Config;

    invoke-direct {p2, p1}, Ljp/colopl/config/Config;-><init>(Landroid/content/Context;)V

    iput-object p2, p0, Ljp/colopl/network/PostLocationAsynTaskBase;->mConfig:Ljp/colopl/config/Config;

    return-void
.end method


# virtual methods
.method protected bridge synthetic after(Landroid/content/Context;Ljava/lang/Object;)V
    .locals 0

    .line 16
    check-cast p2, Ljava/lang/String;

    invoke-virtual {p0, p1, p2}, Ljp/colopl/network/PostLocationAsynTaskBase;->after(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method

.method protected abstract after(Landroid/content/Context;Ljava/lang/String;)V
.end method

.method protected bridge synthetic doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 16
    check-cast p2, [Ljava/lang/Void;

    invoke-virtual {p0, p1, p2}, Ljp/colopl/network/PostLocationAsynTaskBase;->doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Void;)Ljava/lang/String;

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

    .line 34
    invoke-super {p0, p1, p2}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Object;)Ljava/lang/Object;

    .line 35
    invoke-virtual {p0}, Ljp/colopl/network/PostLocationAsynTaskBase;->getPostData()Ljava/util/List;

    move-result-object p1

    .line 36
    iget-object p2, p0, Ljp/colopl/network/PostLocationAsynTaskBase;->mPostUrl:Ljava/lang/String;

    iget-object v0, p0, Ljp/colopl/network/PostLocationAsynTaskBase;->mConfig:Ljp/colopl/config/Config;

    invoke-static {v0}, Ljp/colopl/util/HTTP;->createCookies(Ljp/colopl/config/Config;)Ljava/util/List;

    move-result-object v0

    invoke-static {p2, p1, v0}, Ljp/colopl/util/HTTP;->post(Ljava/lang/String;Ljava/util/List;Ljava/util/List;)Ljava/lang/String;

    move-result-object p1

    .line 37
    invoke-virtual {p0, p1}, Ljp/colopl/network/PostLocationAsynTaskBase;->handleResponseInBackground(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method protected getConfig()Ljp/colopl/config/Config;
    .locals 1

    .line 56
    iget-object v0, p0, Ljp/colopl/network/PostLocationAsynTaskBase;->mConfig:Ljp/colopl/config/Config;

    return-object v0
.end method

.method protected getLocations()Ljava/util/HashMap;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Landroid/location/Location;",
            ">;"
        }
    .end annotation

    .line 60
    iget-object v0, p0, Ljp/colopl/network/PostLocationAsynTaskBase;->mLocations:Ljava/util/HashMap;

    return-object v0
.end method

.method protected abstract getPostData()Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;"
        }
    .end annotation
.end method

.method protected getPostUrl()Ljava/lang/String;
    .locals 1

    .line 52
    iget-object v0, p0, Ljp/colopl/network/PostLocationAsynTaskBase;->mPostUrl:Ljava/lang/String;

    return-object v0
.end method

.method protected abstract handleError(Landroid/content/Context;Ljava/lang/Exception;)V
.end method

.method protected handleResponseInBackground(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    return-object p1
.end method
