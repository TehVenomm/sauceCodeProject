.class Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$9;
.super Ljava/lang/Object;
.source "DoCoMoLocationInfoHandler.java"

# interfaces
.implements Landroid/sax/EndTextElementListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->parse(Ljava/io/InputStream;)Ljp/colopl/api/docomo/DoCoMoLocationInfo;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;


# direct methods
.method constructor <init>(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)V
    .locals 0

    .line 108
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$9;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public end(Ljava/lang/String;)V
    .locals 10

    const/4 v0, 0x0

    const/4 v1, 0x4

    .line 113
    :try_start_0
    invoke-virtual {p1, v0, v1}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result v0

    const/4 v1, 0x5

    const/4 v2, 0x7

    .line 114
    invoke-virtual {p1, v1, v2}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result v1

    const/16 v2, 0x8

    const/16 v3, 0xa

    .line 115
    invoke-virtual {p1, v2, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result v6

    const/16 v2, 0xb

    const/16 v3, 0xd

    .line 116
    invoke-virtual {p1, v2, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result v7

    const/16 v2, 0xe

    const/16 v3, 0x10

    .line 117
    invoke-virtual {p1, v2, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result v8

    const/16 v2, 0x11

    const/16 v3, 0x13

    .line 118
    invoke-virtual {p1, v2, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result v9

    .line 119
    new-instance v2, Ljava/util/Date;

    add-int/lit16 v4, v0, -0x76c

    add-int/lit8 v5, v1, -0x1

    move-object v3, v2

    invoke-direct/range {v3 .. v9}, Ljava/util/Date;-><init>(IIIIII)V

    .line 120
    invoke-virtual {v2}, Ljava/util/Date;->getTime()J

    move-result-wide v0
    :try_end_0
    .catch Ljava/lang/NumberFormatException; {:try_start_0 .. :try_end_0} :catch_0

    .line 124
    iget-object v2, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$9;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-static {v2}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->access$200(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)Ljp/colopl/api/docomo/Feature;

    move-result-object v2

    invoke-virtual {v2, v0, v1}, Ljp/colopl/api/docomo/Feature;->setTime(J)V

    .line 125
    iget-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$9;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-static {v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->access$200(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)Ljp/colopl/api/docomo/Feature;

    move-result-object v0

    invoke-virtual {v0, p1}, Ljp/colopl/api/docomo/Feature;->setTimeStr(Ljava/lang/String;)V

    return-void

    :catch_0
    move-exception p1

    .line 122
    throw p1
.end method
