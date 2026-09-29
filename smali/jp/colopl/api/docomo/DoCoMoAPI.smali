.class public Ljp/colopl/api/docomo/DoCoMoAPI;
.super Ljava/lang/Object;
.source "DoCoMoAPI.java"


# static fields
.field public static final ENTRY_POINT:Ljava/lang/String; = "https://api.spmode.ne.jp/nwLocation/GetLocation"

.field public static final PSEUDO_ACCURACY:F = 250.0f

.field public static final PSEUDO_NAME_AS_LOCATION_PROVIDER:Ljava/lang/String; = "DoCoMoSPApi"

.field private static final REQUEST_XML:Ljava/lang/String; = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><DDF ver=\"1.0\"><RequestInfo><RequestParam><APIKey><APIKey1_ID>%s</APIKey1_ID ><APIKey2>%s</APIKey2></APIKey><OptionProperty><AreaCode></AreaCode><AreaName></AreaName><Adr></Adr><AdrCode></AdrCode><PostCode></PostCode></OptionProperty></RequestParam></RequestInfo></DDF>"

.field public static final RESULT_AUTHORIZE_ERROR:I = 0x3

.field public static final RESULT_REQUEST_INVALID_ERROR:I = -0x3

.field public static final RESULT_SERVICE_ERROR:I = 0x2

.field public static final RESULT_SUCCEEDED:I = 0x0

.field public static final RESULT_SYSTEM_ERROR:I = 0x4

.field public static final RESULT_UNKNOWN_ERROR:I = -0x1

.field public static final REULST_POST_ERROR:I = -0x2

.field public static final SETTING_SITE_APP_URL:Ljava/lang/String; = "https://spmode.ne.jp/setting/apiUsePermitAuthAction.do?apikey=colopl-cOlopg001"

.field public static final SETTING_SITE_URL:Ljava/lang/String; = "https://spmode.ne.jp/setting/apiControlAuthKeyClean.do"


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "dcapikey"

    .line 32
    invoke-static {v0}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static native getApiKey1()Ljava/lang/String;
.end method

.method public static native getApiKey2()Ljava/lang/String;
.end method

.method public static getEntryPoint()Ljava/lang/String;
    .locals 1

    const-string v0, "https://api.spmode.ne.jp/nwLocation/GetLocation"

    return-object v0
.end method

.method public static getRequestXML()Ljava/lang/String;
    .locals 4

    const-string v0, "<?xml version=\"1.0\" encoding=\"UTF-8\"?><DDF ver=\"1.0\"><RequestInfo><RequestParam><APIKey><APIKey1_ID>%s</APIKey1_ID ><APIKey2>%s</APIKey2></APIKey><OptionProperty><AreaCode></AreaCode><AreaName></AreaName><Adr></Adr><AdrCode></AdrCode><PostCode></PostCode></OptionProperty></RequestParam></RequestInfo></DDF>"

    const/4 v1, 0x2

    .line 39
    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {}, Ljp/colopl/api/docomo/DoCoMoAPI;->getApiKey1()Ljava/lang/String;

    move-result-object v2

    const/4 v3, 0x0

    aput-object v2, v1, v3

    invoke-static {}, Ljp/colopl/api/docomo/DoCoMoAPI;->getApiKey2()Ljava/lang/String;

    move-result-object v2

    const/4 v3, 0x1

    aput-object v2, v1, v3

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static isAvailableDoCoMoAPI(Ljp/colopl/drapro/ColoplApplication;)Z
    .locals 1

    .line 47
    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isCarrierDoCoMo()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Ljp/colopl/drapro/ColoplApplication;->isMobileConnectivity()Z

    move-result p0

    if-eqz p0, :cond_0

    const/4 p0, 0x1

    goto :goto_0

    :cond_0
    const/4 p0, 0x0

    :goto_0
    return p0
.end method


# virtual methods
.method public getLocationInfo()Ljp/colopl/api/docomo/DoCoMoLocationInfo;
    .locals 2

    .line 55
    invoke-virtual {p0}, Ljp/colopl/api/docomo/DoCoMoAPI;->post()Ljava/io/InputStream;

    move-result-object v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 59
    :cond_0
    new-instance v1, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-direct {v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;-><init>()V

    .line 60
    invoke-virtual {v1, v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->parse(Ljava/io/InputStream;)Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    move-result-object v0

    return-object v0
.end method

.method public post()Ljava/io/InputStream;
    .locals 2

    const-string v0, "https://api.spmode.ne.jp/nwLocation/GetLocation"

    .line 51
    invoke-static {}, Ljp/colopl/api/docomo/DoCoMoAPI;->getRequestXML()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/HTTP;->postXML(Ljava/lang/String;Ljava/lang/String;)Ljava/io/InputStream;

    move-result-object v0

    return-object v0
.end method
