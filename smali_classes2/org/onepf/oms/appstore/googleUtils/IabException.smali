.class public Lorg/onepf/oms/appstore/googleUtils/IabException;
.super Ljava/lang/Exception;
.source "IabException.java"


# instance fields
.field mResult:Lorg/onepf/oms/appstore/googleUtils/IabResult;


# direct methods
.method public constructor <init>(ILjava/lang/String;)V
    .locals 1

    .line 35
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    invoke-direct {v0, p1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-direct {p0, v0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V

    return-void
.end method

.method public constructor <init>(ILjava/lang/String;Ljava/lang/Exception;)V
    .locals 1

    .line 44
    new-instance v0, Lorg/onepf/oms/appstore/googleUtils/IabResult;

    invoke-direct {v0, p1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    invoke-direct {p0, v0, p3}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabResult;Ljava/lang/Exception;)V

    return-void
.end method

.method public constructor <init>(Lorg/onepf/oms/appstore/googleUtils/IabResult;)V
    .locals 1
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabResult;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    const/4 v0, 0x0

    .line 31
    invoke-direct {p0, p1, v0}, Lorg/onepf/oms/appstore/googleUtils/IabException;-><init>(Lorg/onepf/oms/appstore/googleUtils/IabResult;Ljava/lang/Exception;)V

    return-void
.end method

.method public constructor <init>(Lorg/onepf/oms/appstore/googleUtils/IabResult;Ljava/lang/Exception;)V
    .locals 1
    .param p1    # Lorg/onepf/oms/appstore/googleUtils/IabResult;
        .annotation build Lorg/jetbrains/annotations/NotNull;
        .end annotation
    .end param

    .line 39
    invoke-virtual {p1}, Lorg/onepf/oms/appstore/googleUtils/IabResult;->getMessage()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v0, p2}, Ljava/lang/Exception;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    .line 40
    iput-object p1, p0, Lorg/onepf/oms/appstore/googleUtils/IabException;->mResult:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    return-void
.end method


# virtual methods
.method public getResult()Lorg/onepf/oms/appstore/googleUtils/IabResult;
    .locals 1

    .line 51
    iget-object v0, p0, Lorg/onepf/oms/appstore/googleUtils/IabException;->mResult:Lorg/onepf/oms/appstore/googleUtils/IabResult;

    return-object v0
.end method
