.class Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$3;
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

    .line 59
    iput-object p1, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$3;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public end(Ljava/lang/String;)V
    .locals 1

    .line 62
    iget-object v0, p0, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler$3;->this$0:Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;

    invoke-static {v0}, Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;->access$100(Ljp/colopl/api/docomo/DoCoMoLocationInfoHandler;)Ljp/colopl/api/docomo/ResultInfo;

    move-result-object v0

    invoke-static {p1}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result p1

    invoke-virtual {v0, p1}, Ljp/colopl/api/docomo/ResultInfo;->setTotalCount(I)V

    return-void
.end method
