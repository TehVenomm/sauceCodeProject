.class public Lcom/helpshift/common/platform/network/Request;
.super Ljava/lang/Object;
.source "Request.java"


# instance fields
.field public final headers:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;"
        }
    .end annotation
.end field

.field public final method:Lcom/helpshift/common/platform/network/Method;

.field public final timeout:I

.field public final url:Ljava/lang/String;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/network/Method;Ljava/lang/String;Ljava/util/List;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/platform/network/Method;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;I)V"
        }
    .end annotation

    .line 15
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 16
    iput-object p1, p0, Lcom/helpshift/common/platform/network/Request;->method:Lcom/helpshift/common/platform/network/Method;

    .line 17
    iput-object p2, p0, Lcom/helpshift/common/platform/network/Request;->url:Ljava/lang/String;

    .line 18
    iput-object p3, p0, Lcom/helpshift/common/platform/network/Request;->headers:Ljava/util/List;

    .line 19
    iput p4, p0, Lcom/helpshift/common/platform/network/Request;->timeout:I

    return-void
.end method
