.class public Lcom/helpshift/common/platform/network/Response;
.super Ljava/lang/Object;
.source "Response.java"


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

.field public final responseString:Ljava/lang/String;

.field public final status:I


# direct methods
.method public constructor <init>(ILjava/lang/String;Ljava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;)V"
        }
    .end annotation

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 12
    iput p1, p0, Lcom/helpshift/common/platform/network/Response;->status:I

    .line 13
    iput-object p2, p0, Lcom/helpshift/common/platform/network/Response;->responseString:Ljava/lang/String;

    .line 14
    iput-object p3, p0, Lcom/helpshift/common/platform/network/Response;->headers:Ljava/util/List;

    return-void
.end method
