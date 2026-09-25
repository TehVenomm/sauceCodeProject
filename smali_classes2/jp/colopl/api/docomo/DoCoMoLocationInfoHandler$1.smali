.class Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$1;
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

    .line 47
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$1;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public start(Lorg/xml/sax/Attributes;)V
    .locals 1

    .line 50
    iget-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$1;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    new-instance v0, Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    invoke-direct {v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfo;-><init>()V

    invoke-static {p1, v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->access$002(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;Ljp/colopl/api/docomo/DoCoMoLocationInfo;)Ljp/colopl/api/docomo/DoCoMoLocationInfo;

    return-void
.end method
