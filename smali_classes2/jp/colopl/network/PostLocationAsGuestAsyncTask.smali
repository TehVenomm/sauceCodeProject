.class public Ljp/colopl/network/PostLocationAsGuestAsyncTask;
.super Ljp/colopl/network/PostLocationAsynTaskBase;
.source "PostLocationAsGuestAsyncTask.java"


# instance fields
.field mAid:Ljava/lang/String;

.field mDelegate:Ljp/colopl/network/PostLocationAsGuestAsyncTaskDelegate;

.field mGuid:Ljava/lang/String;

.field mHash:Ljava/lang/String;

.field mTime:Ljava/lang/String;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/util/HashMap;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/util/HashMap<",
            "Ljava/lang/String;",
            "Landroid/location/Location;",
            ">;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .line 29
    invoke-direct {p0, p1, p2, p3}, Ljp/colopl/network/PostLocationAsynTaskBase;-><init>(Landroid/content/Context;Ljava/lang/String;Ljava/util/HashMap;)V

    .line 30
    iput-object p4, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mAid:Ljava/lang/String;

    .line 31
    iput-object p5, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mGuid:Ljava/lang/String;

    .line 32
    iput-object p6, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mTime:Ljava/lang/String;

    .line 33
    iput-object p7, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mHash:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method protected bridge synthetic after(Landroid/content/Context;Ljava/lang/Object;)V
    .locals 0

    .line 19
    check-cast p2, Ljava/lang/String;

    invoke-virtual {p0, p1, p2}, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->after(Landroid/content/Context;Ljava/lang/String;)V

    return-void
.end method

.method protected after(Landroid/content/Context;Ljava/lang/String;)V
    .locals 0

    .line 88
    iget-object p1, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mDelegate:Ljp/colopl/network/PostLocationAsGuestAsyncTaskDelegate;

    if-nez p1, :cond_0

    return-void

    .line 91
    :cond_0
    invoke-virtual {p0}, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->isCancelled()Z

    move-result p1

    if-eqz p1, :cond_1

    return-void

    .line 94
    :cond_1
    iget-object p1, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mDelegate:Ljp/colopl/network/PostLocationAsGuestAsyncTaskDelegate;

    invoke-interface {p1, p2}, Ljp/colopl/network/PostLocationAsGuestAsyncTaskDelegate;->onPostLocationAsGuest(Ljava/lang/String;)V

    return-void
.end method

.method protected getPostData()Ljava/util/List;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lorg/apache/http/NameValuePair;",
            ">;"
        }
    .end annotation

    .line 38
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 39
    invoke-virtual {p0}, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->getLocations()Ljava/util/HashMap;

    move-result-object v1

    invoke-static {v1}, Ljp/colopl/util/LocationUtil;->getMostAccurateLocation(Ljava/util/HashMap;)Landroid/location/Location;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 40
    invoke-static {v0}, Ljp/colopl/util/LocationUtil;->getEncryptedLocations(Ljava/util/List;)Ljava/lang/String;

    move-result-object v0

    .line 42
    new-instance v1, Ljava/util/ArrayList;

    const/4 v2, 0x5

    invoke-direct {v1, v2}, Ljava/util/ArrayList;-><init>(I)V

    .line 43
    new-instance v2, Lorg/apache/http/message/BasicNameValuePair;

    const-string v3, "aid"

    iget-object v4, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mAid:Ljava/lang/String;

    invoke-direct {v2, v3, v4}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 44
    new-instance v2, Lorg/apache/http/message/BasicNameValuePair;

    const-string v3, "puid"

    iget-object v4, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mGuid:Ljava/lang/String;

    invoke-direct {v2, v3, v4}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 45
    new-instance v2, Lorg/apache/http/message/BasicNameValuePair;

    const-string v3, "t"

    iget-object v4, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mTime:Ljava/lang/String;

    invoke-direct {v2, v3, v4}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 46
    new-instance v2, Lorg/apache/http/message/BasicNameValuePair;

    const-string v3, "sk"

    iget-object v4, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mHash:Ljava/lang/String;

    invoke-direct {v2, v3, v4}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 47
    new-instance v2, Lorg/apache/http/message/BasicNameValuePair;

    const-string v3, "location"

    invoke-direct {v2, v3, v0}, Lorg/apache/http/message/BasicNameValuePair;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    return-object v1
.end method

.method protected handleError(Landroid/content/Context;Ljava/lang/Exception;)V
    .locals 0

    .line 103
    iget-object p1, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mDelegate:Ljp/colopl/network/PostLocationAsGuestAsyncTaskDelegate;

    if-nez p1, :cond_0

    return-void

    .line 106
    :cond_0
    invoke-virtual {p0}, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->isCancelled()Z

    move-result p1

    if-eqz p1, :cond_1

    return-void

    .line 109
    :cond_1
    iget-object p1, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mDelegate:Ljp/colopl/network/PostLocationAsGuestAsyncTaskDelegate;

    const/4 p2, 0x0

    invoke-interface {p1, p2}, Ljp/colopl/network/PostLocationAsGuestAsyncTaskDelegate;->onPostLocationAsGuest(Ljava/lang/String;)V

    return-void
.end method

.method protected handleResponseInBackground(Ljava/lang/String;)Ljava/lang/String;
    .locals 3

    .line 54
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    return-object v1

    .line 58
    :cond_0
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v0

    add-int/lit8 v0, v0, -0x1

    invoke-virtual {p1, v0}, Ljava/lang/String;->charAt(I)C

    move-result v0

    const/16 v2, 0xa

    if-ne v0, v2, :cond_1

    const/4 v0, 0x0

    .line 59
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v2

    add-int/lit8 v2, v2, -0x1

    invoke-virtual {p1, v0, v2}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object p1

    .line 63
    :cond_1
    :try_start_0
    invoke-static {p1}, Ljp/colopl/util/Crypto;->decrypt(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1

    if-eqz p1, :cond_2

    .line 72
    :try_start_1
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v2, "status"

    .line 73
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    const-string v2, "code"

    .line 74
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v0
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    const/16 v2, 0x64

    if-eq v0, v2, :cond_2

    return-object v1

    :catch_0
    move-exception p1

    .line 79
    invoke-virtual {p1}, Lorg/json/JSONException;->printStackTrace()V

    return-object v1

    :cond_2
    return-object p1

    :catch_1
    move-exception p1

    .line 65
    invoke-virtual {p1}, Ljava/lang/Exception;->printStackTrace()V

    return-object v1
.end method

.method public setDelegate(Ljp/colopl/network/PostLocationAsGuestAsyncTaskDelegate;)V
    .locals 0

    .line 98
    iput-object p1, p0, Ljp/colopl/network/PostLocationAsGuestAsyncTask;->mDelegate:Ljp/colopl/network/PostLocationAsGuestAsyncTaskDelegate;

    return-void
.end method
