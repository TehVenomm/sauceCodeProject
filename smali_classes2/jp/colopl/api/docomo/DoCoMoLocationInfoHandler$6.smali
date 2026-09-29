.class Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$6;
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

    .line 77
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$6;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public start(Lorg/xml/sax/Attributes;)V
    .locals 1

    .line 80
    iget-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$6;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    new-instance v0, Ljp/colopl/api/docomo/Feature;

    invoke-direct {v0}, Ljp/colopl/api/docomo/Feature;-><init>()V

    invoke-static {p1, v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->access$202(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;Ljp/colopl/api/docomo/Feature;)Ljp/colopl/api/docomo/Feature;

    .line 81
    iget-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$6;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-static {p1}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->access$000(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    move-result-object p1

    invoke-virtual {p1}, Ljp/colopl/api/docomo/DoCoMoLocationInfo;->getFeatureList()Ljava/util/ArrayList;

    move-result-object p1

    iget-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$6;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-static {v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->access$200(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)Ljp/colopl/api/docomo/Feature;

    move-result-object v0

    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    return-void
.end method
