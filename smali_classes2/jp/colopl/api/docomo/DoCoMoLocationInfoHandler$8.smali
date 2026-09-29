.class Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$8;
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

    .line 96
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$8;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public end(Ljava/lang/String;)V
    .locals 4

    const/4 v0, 0x0

    .line 100
    invoke-virtual {p1, v0}, Ljava/lang/String;->charAt(I)C

    move-result v0

    const/4 v1, 0x1

    const/16 v2, 0x57

    if-ne v0, v2, :cond_0

    const/4 v0, -0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x1

    .line 103
    :goto_0
    invoke-virtual {p1, v1}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object p1

    int-to-double v0, v0

    .line 104
    invoke-static {p1}, Ljava/lang/Double;->parseDouble(Ljava/lang/String;)D

    move-result-wide v2

    invoke-static {v0, v1}, Ljava/lang/Double;->isNaN(D)Z

    mul-double v0, v0, v2

    .line 105
    iget-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$8;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-static {p1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->access$200(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)Ljp/colopl/api/docomo/Feature;

    move-result-object p1

    invoke-virtual {p1, v0, v1}, Ljp/colopl/api/docomo/Feature;->setLongitude(D)V

    return-void
.end method
