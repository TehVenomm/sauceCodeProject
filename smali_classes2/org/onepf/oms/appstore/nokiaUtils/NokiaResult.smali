.class public Lorg/onepf/oms/appstore/nokiaUtils/NokiaResult;
.super Lorg/onepf/oms/appstore/googleUtils/IabResult;
.source "NokiaResult.java"


# static fields
.field public static final RESULT_NO_SIM:I = 0x9


# direct methods
.method public constructor <init>(ILjava/lang/String;)V
    .locals 2

    const/16 v0, 0x9

    if-ne p1, v0, :cond_0

    const/4 v1, 0x6

    goto :goto_0

    :cond_0
    move v1, p1

    :goto_0
    if-ne p1, v0, :cond_1

    .line 28
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "No sim. "

    invoke-virtual {p1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    :cond_1
    invoke-direct {p0, v1, p2}, Lorg/onepf/oms/appstore/googleUtils/IabResult;-><init>(ILjava/lang/String;)V

    return-void
.end method
