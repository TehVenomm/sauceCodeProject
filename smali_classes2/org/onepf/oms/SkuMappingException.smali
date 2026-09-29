.class public Lorg/onepf/oms/SkuMappingException;
.super Ljava/lang/IllegalArgumentException;
.source "SkuMappingException.java"


# static fields
.field public static final REASON_SKU:I = 0x1

.field public static final REASON_STORE_NAME:I = 0x2

.field public static final REASON_STORE_SKU:I = 0x3


# direct methods
.method public constructor <init>()V
    .locals 1

    const-string v0, "Error while map sku."

    .line 12
    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;)V
    .locals 0

    .line 16
    invoke-direct {p0, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    return-void
.end method

.method public static newInstance(I)Lorg/onepf/oms/SkuMappingException;
    .locals 1
    .annotation build Lorg/jetbrains/annotations/NotNull;
    .end annotation

    packed-switch p0, :pswitch_data_0

    .line 33
    new-instance p0, Lorg/onepf/oms/SkuMappingException;

    invoke-direct {p0}, Lorg/onepf/oms/SkuMappingException;-><init>()V

    return-object p0

    .line 30
    :pswitch_0
    new-instance p0, Lorg/onepf/oms/SkuMappingException;

    const-string v0, "Store sku can\'t be null or empty value."

    invoke-direct {p0, v0}, Lorg/onepf/oms/SkuMappingException;-><init>(Ljava/lang/String;)V

    return-object p0

    .line 27
    :pswitch_1
    new-instance p0, Lorg/onepf/oms/SkuMappingException;

    const-string v0, "Store name can\'t be null or empty value."

    invoke-direct {p0, v0}, Lorg/onepf/oms/SkuMappingException;-><init>(Ljava/lang/String;)V

    return-object p0

    .line 24
    :pswitch_2
    new-instance p0, Lorg/onepf/oms/SkuMappingException;

    const-string v0, "Sku can\'t be null or empty value."

    invoke-direct {p0, v0}, Lorg/onepf/oms/SkuMappingException;-><init>(Ljava/lang/String;)V

    return-object p0

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
