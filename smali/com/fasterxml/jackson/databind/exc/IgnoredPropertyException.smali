.class public Lcom/fasterxml/jackson/databind/exc/IgnoredPropertyException;
.super Lcom/fasterxml/jackson/databind/exc/PropertyBindingException;
.source "IgnoredPropertyException.java"


# static fields
.field private static final serialVersionUID:J = 0x1L


# direct methods
.method public constructor <init>(Ljava/lang/String;Lcom/fasterxml/jackson/core/JsonLocation;Ljava/lang/Class;Ljava/lang/String;Ljava/util/Collection;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Lcom/fasterxml/jackson/core/JsonLocation;",
            "Ljava/lang/Class<",
            "*>;",
            "Ljava/lang/String;",
            "Ljava/util/Collection<",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 25
    invoke-direct/range {p0 .. p5}, Lcom/fasterxml/jackson/databind/exc/PropertyBindingException;-><init>(Ljava/lang/String;Lcom/fasterxml/jackson/core/JsonLocation;Ljava/lang/Class;Ljava/lang/String;Ljava/util/Collection;)V

    return-void
.end method

.method public static from(Lcom/fasterxml/jackson/core/JsonParser;Ljava/lang/Object;Ljava/lang/String;Ljava/util/Collection;)Lcom/fasterxml/jackson/databind/exc/IgnoredPropertyException;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/core/JsonParser;",
            "Ljava/lang/Object;",
            "Ljava/lang/String;",
            "Ljava/util/Collection<",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/fasterxml/jackson/databind/exc/IgnoredPropertyException;"
        }
    .end annotation

    if-eqz p1, :cond_1

    .line 46
    instance-of v0, p1, Ljava/lang/Class;

    if-eqz v0, :cond_0

    .line 47
    move-object v0, p1

    check-cast v0, Ljava/lang/Class;

    :goto_0
    move-object v4, v0

    goto :goto_1

    .line 49
    :cond_0
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    goto :goto_0

    .line 51
    :goto_1
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Ignored field \""

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "\" (class "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ") encountered; mapper configured not to allow this"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 53
    new-instance v0, Lcom/fasterxml/jackson/databind/exc/IgnoredPropertyException;

    invoke-virtual {p0}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentLocation()Lcom/fasterxml/jackson/core/JsonLocation;

    move-result-object v3

    move-object v1, v0

    move-object v5, p2

    move-object v6, p3

    invoke-direct/range {v1 .. v6}, Lcom/fasterxml/jackson/databind/exc/IgnoredPropertyException;-><init>(Ljava/lang/String;Lcom/fasterxml/jackson/core/JsonLocation;Ljava/lang/Class;Ljava/lang/String;Ljava/util/Collection;)V

    .line 56
    invoke-virtual {v0, p1, p2}, Lcom/fasterxml/jackson/databind/exc/IgnoredPropertyException;->prependPath(Ljava/lang/Object;Ljava/lang/String;)V

    return-object v0

    .line 43
    :cond_1
    new-instance p0, Ljava/lang/IllegalArgumentException;

    invoke-direct {p0}, Ljava/lang/IllegalArgumentException;-><init>()V

    throw p0
.end method
