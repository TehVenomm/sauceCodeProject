.class Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Fields;
.super Ljava/lang/Object;
.source "CustomConcurrentHashMap.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/common/collect/CustomConcurrentHashMap$Impl;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "Fields"
.end annotation


# static fields
.field static final segmentMask:Ljava/lang/reflect/Field;

.field static final segmentShift:Ljava/lang/reflect/Field;

.field static final segments:Ljava/lang/reflect/Field;

.field static final strategy:Ljava/lang/reflect/Field;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "segmentShift"

    .line 1971
    invoke-static {v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Fields;->findField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v0

    sput-object v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Fields;->segmentShift:Ljava/lang/reflect/Field;

    const-string v0, "segmentMask"

    .line 1972
    invoke-static {v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Fields;->findField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v0

    sput-object v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Fields;->segmentMask:Ljava/lang/reflect/Field;

    const-string v0, "segments"

    .line 1973
    invoke-static {v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Fields;->findField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v0

    sput-object v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Fields;->segments:Ljava/lang/reflect/Field;

    const-string v0, "strategy"

    .line 1974
    invoke-static {v0}, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Fields;->findField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v0

    sput-object v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl$Fields;->strategy:Ljava/lang/reflect/Field;

    return-void
.end method

.method constructor <init>()V
    .locals 0

    .line 1969
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static findField(Ljava/lang/String;)Ljava/lang/reflect/Field;
    .locals 1

    .line 1978
    :try_start_0
    const-class v0, Lcom/google/common/collect/CustomConcurrentHashMap$Impl;

    invoke-virtual {v0, p0}, Ljava/lang/Class;->getDeclaredField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object p0

    const/4 v0, 0x1

    .line 1979
    invoke-virtual {p0, v0}, Ljava/lang/reflect/Field;->setAccessible(Z)V
    :try_end_0
    .catch Ljava/lang/NoSuchFieldException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    .line 1982
    new-instance v0, Ljava/lang/AssertionError;

    invoke-direct {v0, p0}, Ljava/lang/AssertionError;-><init>(Ljava/lang/Object;)V

    throw v0
.end method
