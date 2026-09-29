.class public Lcom/helpshift/common/platform/network/UploadRequest;
.super Lcom/helpshift/common/platform/network/Request;
.source "UploadRequest.java"


# static fields
.field public static final BOUNDARY:Ljava/lang/String; = "*****"


# instance fields
.field public final data:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field public final mimeType:Ljava/lang/String;


# direct methods
.method public constructor <init>(Lcom/helpshift/common/platform/network/Method;Ljava/lang/String;Ljava/util/Map;Ljava/lang/String;Ljava/util/List;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/common/platform/network/Method;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/common/platform/network/KeyValuePair;",
            ">;I)V"
        }
    .end annotation

    .line 18
    invoke-direct {p0, p1, p2, p5, p6}, Lcom/helpshift/common/platform/network/Request;-><init>(Lcom/helpshift/common/platform/network/Method;Ljava/lang/String;Ljava/util/List;I)V

    .line 19
    iput-object p3, p0, Lcom/helpshift/common/platform/network/UploadRequest;->data:Ljava/util/Map;

    .line 20
    iput-object p4, p0, Lcom/helpshift/common/platform/network/UploadRequest;->mimeType:Ljava/lang/String;

    return-void
.end method
