.class Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$2;
.super Ljava/lang/Object;
.source "DoCoMoLocationInfoHandler.java"

# interfaces
.implements Landroid/sax/StartElementListener;


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

    .line 53
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$2;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public start(Lorg/xml/sax/Attributes;)V
    .locals 1

    .line 56
    iget-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$2;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    iget-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$2;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-static {v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->access$000(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    move-result-object v0

    invoke-virtual {v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->getResultInfo()Ljp/colopl/api/docomo/ResultInfo;

    move-result-object v0

    invoke-static {p1, v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->access$102(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;Ljp/colopl/api/docomo/ResultInfo;)Ljp/colopl/api/docomo/ResultInfo;

    return-void
.end method
