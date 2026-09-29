.class public Lcom/helpshift/common/platform/network/GETRequest;
.super Lcom/helpshift/common/platform/network/Request;
.source "GETRequest.java"


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/util/List;I)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;I)V"
        }
    .end annotation

    .line 10
    sget-object v0, Lcom/helpshift/common/platform/network/Method;->GET:Lcom/helpshift/common/platform/network/Method;

    invoke-direct {p0, v0, p1, p2, p3}, Lcom/helpshift/common/platform/network/Request;-><init>(Lcom/helpshift/common/platform/network/Method;Ljava/lang/String;Ljava/util/List;I)V

    return-void
.end method
