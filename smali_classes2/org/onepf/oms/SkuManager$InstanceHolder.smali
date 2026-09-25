.class final Lorg/onepf/oms/SkuManager$InstanceHolder;
.super Ljava/lang/Object;
.source "SkuManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lorg/onepf/oms/SkuManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1a
    name = "InstanceHolder"
.end annotation


# static fields
.field static final SKU_MANAGER:Lorg/onepf/oms/SkuManager;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 228
    new-instance v0, Lorg/onepf/oms/SkuManager;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Lorg/onepf/oms/SkuManager;-><init>(Lorg/onepf/oms/SkuManager$1;)V

    sput-object v0, Lorg/onepf/oms/SkuManager$InstanceHolder;->SKU_MANAGER:Lorg/onepf/oms/SkuManager;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    .line 227
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
