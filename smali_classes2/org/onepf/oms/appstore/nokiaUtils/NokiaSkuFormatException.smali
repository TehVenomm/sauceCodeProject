.class public Lorg/onepf/oms/appstore/nokiaUtils/NokiaSkuFormatException;
.super Lorg/onepf/oms/SkuMappingException;
.source "NokiaSkuFormatException.java"


# direct methods
.method public constructor <init>()V
    .locals 1

    const-string v0, "Nokia Store SKU can contain only digits."

    .line 10
    invoke-direct {p0, v0}, Lorg/onepf/oms/SkuMappingException;-><init>(Ljava/lang/String;)V

    return-void
.end method
