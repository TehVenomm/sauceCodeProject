.class public Lcom/helpshift/network/response/Response;
.super Ljava/lang/Object;
.source "Response.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/network/response/Response$ErrorListener;,
        Lcom/helpshift/network/response/Response$Listener;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;"
    }
.end annotation


# instance fields
.field public final error:Lcom/helpshift/network/errors/NetworkError;

.field public final result:Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "TT;"
        }
    .end annotation
.end field


# direct methods
.method private constructor <init>(Lcom/helpshift/network/errors/NetworkError;Ljava/lang/Integer;)V
    .locals 0

    .line 44
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 p2, 0x0

    .line 45
    iput-object p2, p0, Lcom/helpshift/network/response/Response;->result:Ljava/lang/Object;

    .line 46
    iput-object p1, p0, Lcom/helpshift/network/response/Response;->error:Lcom/helpshift/network/errors/NetworkError;

    return-void
.end method

.method private constructor <init>(Ljava/lang/Object;Ljava/lang/Integer;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;",
            "Ljava/lang/Integer;",
            ")V"
        }
    .end annotation

    .line 39
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 40
    iput-object p1, p0, Lcom/helpshift/network/response/Response;->result:Ljava/lang/Object;

    const/4 p1, 0x0

    .line 41
    iput-object p1, p0, Lcom/helpshift/network/response/Response;->error:Lcom/helpshift/network/errors/NetworkError;

    return-void
.end method

.method public static error(Lcom/helpshift/network/errors/NetworkError;Ljava/lang/Integer;)Lcom/helpshift/network/response/Response;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/helpshift/network/errors/NetworkError;",
            "Ljava/lang/Integer;",
            ")",
            "Lcom/helpshift/network/response/Response<",
            "TT;>;"
        }
    .end annotation

    .line 54
    new-instance v0, Lcom/helpshift/network/response/Response;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/network/response/Response;-><init>(Lcom/helpshift/network/errors/NetworkError;Ljava/lang/Integer;)V

    return-object v0
.end method

.method public static success(Ljava/lang/Object;Ljava/lang/Integer;)Lcom/helpshift/network/response/Response;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(TT;",
            "Ljava/lang/Integer;",
            ")",
            "Lcom/helpshift/network/response/Response<",
            "TT;>;"
        }
    .end annotation

    .line 50
    new-instance v0, Lcom/helpshift/network/response/Response;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/network/response/Response;-><init>(Ljava/lang/Object;Ljava/lang/Integer;)V

    return-object v0
.end method


# virtual methods
.method public isSuccess()Z
    .locals 1

    .line 58
    iget-object v0, p0, Lcom/helpshift/network/response/Response;->error:Lcom/helpshift/network/errors/NetworkError;

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method
