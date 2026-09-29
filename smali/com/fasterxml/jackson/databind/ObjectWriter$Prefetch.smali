.class public final Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;
.super Ljava/lang/Object;
.source "ObjectWriter.java"

# interfaces
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/fasterxml/jackson/databind/ObjectWriter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Prefetch"
.end annotation


# static fields
.field public static final empty:Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;

.field private static final serialVersionUID:J = 0x1L


# instance fields
.field public final rootType:Lcom/fasterxml/jackson/databind/JavaType;

.field public final typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

.field public final valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 1343
    new-instance v0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;

    const/4 v1, 0x0

    invoke-direct {v0, v1, v1, v1}, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;-><init>(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/JsonSerializer;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V

    sput-object v0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;->empty:Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;

    return-void
.end method

.method private constructor <init>(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/JsonSerializer;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;",
            "Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;",
            ")V"
        }
    .end annotation

    .line 1366
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1367
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;->rootType:Lcom/fasterxml/jackson/databind/JavaType;

    .line 1368
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;->valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    .line 1369
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;->typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    return-void
.end method

.method public static construct(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/JsonSerializer;)Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Lcom/fasterxml/jackson/databind/JsonSerializer<",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;"
        }
    .end annotation

    if-nez p0, :cond_0

    if-nez p1, :cond_0

    .line 1374
    sget-object p0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;->empty:Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;

    return-object p0

    .line 1376
    :cond_0
    new-instance v0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;

    const/4 v1, 0x0

    invoke-direct {v0, p0, p1, v1}, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;-><init>(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/JsonSerializer;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V

    return-object v0
.end method

.method public static construct(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;
    .locals 2

    if-nez p0, :cond_0

    if-nez p1, :cond_0

    .line 1381
    sget-object p0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;->empty:Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;

    return-object p0

    .line 1383
    :cond_0
    new-instance v0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;

    const/4 v1, 0x0

    invoke-direct {v0, p0, v1, p1}, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;-><init>(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/databind/JsonSerializer;Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;)V

    return-object v0
.end method


# virtual methods
.method public hasSerializer()Z
    .locals 1

    .line 1387
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;->valueSerializer:Lcom/fasterxml/jackson/databind/JsonSerializer;

    if-nez v0, :cond_1

    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$Prefetch;->typeSerializer:Lcom/fasterxml/jackson/databind/jsontype/TypeSerializer;

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method
