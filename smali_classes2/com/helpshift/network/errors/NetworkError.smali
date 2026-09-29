.class public Lcom/helpshift/network/errors/NetworkError;
.super Ljava/lang/Exception;
.source "NetworkError.java"


# instance fields
.field private reason:Ljava/lang/Integer;


# direct methods
.method public constructor <init>(Ljava/lang/Integer;)V
    .locals 1

    .line 9
    invoke-direct {p0}, Ljava/lang/Exception;-><init>()V

    .line 7
    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->GENERIC_NETWORK_ERROR:Ljava/lang/Integer;

    iput-object v0, p0, Lcom/helpshift/network/errors/NetworkError;->reason:Ljava/lang/Integer;

    .line 10
    iput-object p1, p0, Lcom/helpshift/network/errors/NetworkError;->reason:Ljava/lang/Integer;

    return-void
.end method

.method public constructor <init>(Ljava/lang/Integer;Ljava/lang/String;)V
    .locals 0

    .line 14
    invoke-direct {p0, p2}, Ljava/lang/Exception;-><init>(Ljava/lang/String;)V

    .line 7
    sget-object p2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->GENERIC_NETWORK_ERROR:Ljava/lang/Integer;

    iput-object p2, p0, Lcom/helpshift/network/errors/NetworkError;->reason:Ljava/lang/Integer;

    .line 15
    iput-object p1, p0, Lcom/helpshift/network/errors/NetworkError;->reason:Ljava/lang/Integer;

    return-void
.end method

.method public constructor <init>(Ljava/lang/Integer;Ljava/lang/Throwable;)V
    .locals 0

    .line 23
    invoke-direct {p0, p2}, Ljava/lang/Exception;-><init>(Ljava/lang/Throwable;)V

    .line 7
    sget-object p2, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->GENERIC_NETWORK_ERROR:Ljava/lang/Integer;

    iput-object p2, p0, Lcom/helpshift/network/errors/NetworkError;->reason:Ljava/lang/Integer;

    .line 24
    iput-object p1, p0, Lcom/helpshift/network/errors/NetworkError;->reason:Ljava/lang/Integer;

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/Throwable;)V
    .locals 0

    .line 19
    invoke-direct {p0, p1, p2}, Ljava/lang/Exception;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 7
    sget-object p1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->GENERIC_NETWORK_ERROR:Ljava/lang/Integer;

    iput-object p1, p0, Lcom/helpshift/network/errors/NetworkError;->reason:Ljava/lang/Integer;

    return-void
.end method

.method public constructor <init>(Ljava/lang/Throwable;)V
    .locals 0

    .line 28
    invoke-direct {p0, p1}, Ljava/lang/Exception;-><init>(Ljava/lang/Throwable;)V

    .line 7
    sget-object p1, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->GENERIC_NETWORK_ERROR:Ljava/lang/Integer;

    iput-object p1, p0, Lcom/helpshift/network/errors/NetworkError;->reason:Ljava/lang/Integer;

    return-void
.end method


# virtual methods
.method public getReason()Ljava/lang/Integer;
    .locals 1

    .line 32
    iget-object v0, p0, Lcom/helpshift/network/errors/NetworkError;->reason:Ljava/lang/Integer;

    return-object v0
.end method
