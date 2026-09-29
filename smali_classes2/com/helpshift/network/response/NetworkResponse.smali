.class public Lcom/helpshift/network/response/NetworkResponse;
.super Ljava/lang/Object;
.source "NetworkResponse.java"


# instance fields
.field public final data:[B

.field public final headers:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field public final notModified:Z

.field public final requestIdentifier:Ljava/lang/Integer;

.field public final statusCode:I


# direct methods
.method public constructor <init>(I[BLjava/util/Map;ZLjava/lang/Integer;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I[B",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;Z",
            "Ljava/lang/Integer;",
            ")V"
        }
    .end annotation

    .line 35
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 36
    iput p1, p0, Lcom/helpshift/network/response/NetworkResponse;->statusCode:I

    .line 37
    iput-object p2, p0, Lcom/helpshift/network/response/NetworkResponse;->data:[B

    .line 38
    iput-object p3, p0, Lcom/helpshift/network/response/NetworkResponse;->headers:Ljava/util/Map;

    .line 39
    iput-boolean p4, p0, Lcom/helpshift/network/response/NetworkResponse;->notModified:Z

    .line 40
    iput-object p5, p0, Lcom/helpshift/network/response/NetworkResponse;->requestIdentifier:Ljava/lang/Integer;

    return-void
.end method
