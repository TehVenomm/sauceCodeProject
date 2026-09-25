.class public Lcom/helpshift/common/platform/network/PUTRequest;
.super Lcom/helpshift/common/platform/network/Request;
.source "PUTRequest.java"


# instance fields
.field public final query:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/List;I)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;I)V"
        }
    .end annotation

    .line 13
    sget-object v0, Lcom/helpshift/common/platform/network/Method;->PUT:Lcom/helpshift/common/platform/network/Method;

    invoke-direct {p0, v0, p1, p3, p4}, Lcom/helpshift/common/platform/network/Request;-><init>(Lcom/helpshift/common/platform/network/Method;Ljava/lang/String;Ljava/util/List;I)V

    .line 14
    iput-object p2, p0, Lcom/helpshift/common/platform/network/PUTRequest;->query:Ljava/lang/String;

    return-void
.end method
