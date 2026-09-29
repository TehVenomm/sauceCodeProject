.class public Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;
.super Lorg/xml/sax/helpers/DefaultHandler;
.source "DoCoMoLocationInfoHandler.java"


# static fields
.field private static TAG:Ljava/lang/String; = "LocationInfoHandler"


# instance fields
.field private feature:Ljp/colopl/api/docomo/Feature;

.field private result:Ljp/colopl/api/docomo/DoCoMoLocationInfo;

.field private resultInfo:Ljp/colopl/api/docomo/ResultInfo;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 19
    invoke-direct {p0}, Lorg/xml/sax/helpers/DefaultHandler;-><init>()V

    const/4 v0, 0x0

    .line 22
    iput-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->result:Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    .line 23
    iput-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->resultInfo:Ljp/colopl/api/docomo/ResultInfo;

    .line 24
    iput-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->feature:Ljp/colopl/api/docomo/Feature;

    return-void
.end method

.method static synthetic access$000(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)Ljp/colopl/api/docomo/DoCoMoLocationInfo;
    .locals 0

    .line 19
    iget-object p0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->result:Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    return-object p0
.end method

.method static synthetic access$002(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;Ljp/colopl/api/docomo/DoCoMoLocationInfo;)Ljp/colopl/api/docomo/DoCoMoLocationInfo;
    .locals 0

    .line 19
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->result:Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    return-object p1
.end method

.method static synthetic access$100(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)Ljp/colopl/api/docomo/ResultInfo;
    .locals 0

    .line 19
    iget-object p0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->resultInfo:Ljp/colopl/api/docomo/ResultInfo;

    return-object p0
.end method

.method static synthetic access$102(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;Ljp/colopl/api/docomo/ResultInfo;)Ljp/colopl/api/docomo/ResultInfo;
    .locals 0

    .line 19
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->resultInfo:Ljp/colopl/api/docomo/ResultInfo;

    return-object p1
.end method

.method static synthetic access$200(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)Ljp/colopl/api/docomo/Feature;
    .locals 0

    .line 19
    iget-object p0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->feature:Ljp/colopl/api/docomo/Feature;

    return-object p0
.end method

.method static synthetic access$202(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;Ljp/colopl/api/docomo/Feature;)Ljp/colopl/api/docomo/Feature;
    .locals 0

    .line 19
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->feature:Ljp/colopl/api/docomo/Feature;

    return-object p1
.end method


# virtual methods
.method public parse(Ljava/io/InputStream;)Ljp/colopl/api/docomo/DoCoMoLocationInfo;
    .locals 16

    move-object/from16 v1, p0

    .line 28
    new-instance v0, Landroid/sax/RootElement;

    const-string v2, "DDF"

    invoke-direct {v0, v2}, Landroid/sax/RootElement;-><init>(Ljava/lang/String;)V

    const-string v2, "ResultInfo"

    .line 29
    invoke-virtual {v0, v2}, Landroid/sax/RootElement;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v2

    const-string v3, "TotalCount"

    .line 30
    invoke-virtual {v2, v3}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v3

    const-string v4, "ResultCode"

    .line 31
    invoke-virtual {v2, v4}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v4

    const-string v5, "Error"

    .line 32
    invoke-virtual {v2, v5}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v5

    const-string v6, "Message"

    .line 33
    invoke-virtual {v5, v6}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v5

    const-string v6, "Feature"

    .line 35
    invoke-virtual {v0, v6}, Landroid/sax/RootElement;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v6

    const-string v7, "Geometry"

    .line 36
    invoke-virtual {v6, v7}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v7

    const-string v8, "Lat"

    .line 37
    invoke-virtual {v7, v8}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v8

    const-string v9, "Lon"

    .line 38
    invoke-virtual {v7, v9}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v9

    const-string v10, "Time"

    .line 39
    invoke-virtual {v7, v10}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v7

    const-string v10, "OptionProperty"

    .line 40
    invoke-virtual {v6, v10}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v10

    const-string v11, "AreaCode"

    .line 41
    invoke-virtual {v10, v11}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v11

    const-string v12, "AreaName"

    .line 42
    invoke-virtual {v10, v12}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v12

    const-string v13, "Adr"

    .line 43
    invoke-virtual {v10, v13}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v13

    const-string v14, "AdrCode"

    .line 44
    invoke-virtual {v10, v14}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v14

    const-string v15, "PostCode"

    .line 45
    invoke-virtual {v10, v15}, Landroid/sax/Element;->getChild(Ljava/lang/String;)Landroid/sax/Element;

    move-result-object v10

    .line 47
    new-instance v15, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$1;

    invoke-direct {v15, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$1;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v0, v15}, Landroid/sax/RootElement;->setStartElementListener(Landroid/sax/StartElementListener;)V

    .line 53
    new-instance v15, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$2;

    invoke-direct {v15, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$2;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v2, v15}, Landroid/sax/Element;->setStartElementListener(Landroid/sax/StartElementListener;)V

    .line 59
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$3;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$3;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v3, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 65
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$4;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$4;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v4, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 71
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$5;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$5;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v5, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 77
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$6;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$6;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v6, v2}, Landroid/sax/Element;->setStartElementListener(Landroid/sax/StartElementListener;)V

    .line 84
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$7;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$7;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v8, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 96
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$8;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$8;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v9, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 108
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$9;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$9;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v7, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 128
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$10;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$10;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v11, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 134
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$11;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$11;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v12, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 140
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$12;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$12;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v13, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 146
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$13;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$13;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v14, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 152
    new-instance v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$14;

    invoke-direct {v2, v1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$14;-><init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V

    invoke-virtual {v10, v2}, Landroid/sax/Element;->setEndTextElementListener(Landroid/sax/EndTextElementListener;)V

    .line 162
    :try_start_0
    sget-object v2, Landroid/util/Xml$Encoding;->UTF_8:Landroid/util/Xml$Encoding;

    invoke-virtual {v0}, Landroid/sax/RootElement;->getContentHandler()Lorg/xml/sax/ContentHandler;

    move-result-object v0

    move-object/from16 v3, p1

    invoke-static {v3, v2, v0}, Landroid/util/Xml;->parse(Ljava/io/InputStream;Landroid/util/Xml$Encoding;Lorg/xml/sax/ContentHandler;)V

    .line 163
    iget-object v0, v1, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->result:Ljp/colopl/api/docomo/DoCoMoLocationInfo;
    :try_end_0
    .catch Lorg/xml/sax/SAXException; {:try_start_0 .. :try_end_0} :catch_4
    .catch Ljava/lang/NumberFormatException; {:try_start_0 .. :try_end_0} :catch_3
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_2
    .catch Ljava/lang/AssertionError; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    .line 173
    sget-object v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->TAG:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/Exception;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v2, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :catch_1
    move-exception v0

    .line 171
    sget-object v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->TAG:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/AssertionError;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v2, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :catch_2
    move-exception v0

    .line 169
    sget-object v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->TAG:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/io/IOException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v2, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :catch_3
    move-exception v0

    .line 167
    sget-object v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->TAG:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/NumberFormatException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v2, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    :catch_4
    move-exception v0

    .line 165
    sget-object v2, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->TAG:Ljava/lang/String;

    invoke-virtual {v0}, Lorg/xml/sax/SAXException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v2, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    :goto_0
    const/4 v0, 0x0

    return-object v0
.end method
