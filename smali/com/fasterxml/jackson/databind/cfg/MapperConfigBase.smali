.class public abstract Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
.super Lcom/fasterxml/jackson/databind/cfg/MapperConfig;
.source "MapperConfigBase.java"

# interfaces
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<CFG::",
        "Lcom/fasterxml/jackson/databind/cfg/ConfigFeature;",
        "T:",
        "Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase<",
        "TCFG;TT;>;>",
        "Lcom/fasterxml/jackson/databind/cfg/MapperConfig<",
        "TT;>;",
        "Ljava/io/Serializable;"
    }
.end annotation


# static fields
.field private static final DEFAULT_MAPPER_FEATURES:I

.field private static final serialVersionUID:J = 0x5423f7c7724f4f7aL


# instance fields
.field protected final _attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

.field protected final _mixInAnnotations:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Lcom/fasterxml/jackson/databind/type/ClassKey;",
            "Ljava/lang/Class<",
            "*>;>;"
        }
    .end annotation
.end field

.field protected final _rootName:Ljava/lang/String;

.field protected final _subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

.field protected final _view:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 28
    const-class v0, Lcom/fasterxml/jackson/databind/MapperFeature;

    invoke-static {v0}, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->collectFeatureDefaults(Ljava/lang/Class;)I

    move-result v0

    sput v0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->DEFAULT_MAPPER_FEATURES:I

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/cfg/BaseSettings;Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;Ljava/util/Map;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/BaseSettings;",
            "Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;",
            "Ljava/util/Map<",
            "Lcom/fasterxml/jackson/databind/type/ClassKey;",
            "Ljava/lang/Class<",
            "*>;>;)V"
        }
    .end annotation

    .line 84
    sget v0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->DEFAULT_MAPPER_FEATURES:I

    invoke-direct {p0, p1, v0}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;-><init>(Lcom/fasterxml/jackson/databind/cfg/BaseSettings;I)V

    .line 85
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    .line 86
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    const/4 p1, 0x0

    .line 87
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    .line 88
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    .line 90
    invoke-static {}, Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;->getEmpty()Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    move-result-object p1

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase<",
            "TCFG;TT;>;)V"
        }
    .end annotation

    .line 99
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;-><init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;)V

    .line 100
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    .line 101
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    .line 102
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    .line 103
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    .line 104
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;I)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase<",
            "TCFG;TT;>;I)V"
        }
    .end annotation

    .line 119
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_base:Lcom/fasterxml/jackson/databind/cfg/BaseSettings;

    invoke-direct {p0, v0, p2}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;-><init>(Lcom/fasterxml/jackson/databind/cfg/BaseSettings;I)V

    .line 120
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    .line 121
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    .line 122
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    .line 123
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    .line 124
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;Lcom/fasterxml/jackson/databind/cfg/BaseSettings;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase<",
            "TCFG;TT;>;",
            "Lcom/fasterxml/jackson/databind/cfg/BaseSettings;",
            ")V"
        }
    .end annotation

    .line 109
    iget v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mapperFeatures:I

    invoke-direct {p0, p2, v0}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;-><init>(Lcom/fasterxml/jackson/databind/cfg/BaseSettings;I)V

    .line 110
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    .line 111
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    .line 112
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    .line 113
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    .line 114
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase<",
            "TCFG;TT;>;",
            "Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;",
            ")V"
        }
    .end annotation

    .line 173
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;-><init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;)V

    .line 174
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    .line 175
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    .line 176
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    .line 177
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    .line 178
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase<",
            "TCFG;TT;>;",
            "Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;",
            ")V"
        }
    .end annotation

    .line 128
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;-><init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;)V

    .line 129
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    .line 130
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    .line 131
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    .line 132
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    .line 133
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;Ljava/lang/Class;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase<",
            "TCFG;TT;>;",
            "Ljava/lang/Class<",
            "*>;)V"
        }
    .end annotation

    .line 147
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;-><init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;)V

    .line 148
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    .line 149
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    .line 150
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    .line 151
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    .line 152
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;Ljava/lang/String;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase<",
            "TCFG;TT;>;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .line 137
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;-><init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;)V

    .line 138
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    .line 139
    iget-object v0, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    iput-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    .line 140
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    .line 141
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    .line 142
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase<",
            "TCFG;TT;>;",
            "Ljava/util/Map<",
            "Lcom/fasterxml/jackson/databind/type/ClassKey;",
            "Ljava/lang/Class<",
            "*>;>;)V"
        }
    .end annotation

    .line 160
    invoke-direct {p0, p1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfig;-><init>(Lcom/fasterxml/jackson/databind/cfg/MapperConfig;)V

    .line 161
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    .line 162
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    .line 163
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    .line 164
    iget-object p2, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    iput-object p2, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    .line 165
    iget-object p1, p1, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    iput-object p1, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-void
.end method


# virtual methods
.method public final findMixInClassFor(Ljava/lang/Class;)Ljava/lang/Class;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .line 404
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    if-nez v0, :cond_0

    const/4 p1, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    new-instance v1, Lcom/fasterxml/jackson/databind/type/ClassKey;

    invoke-direct {v1, p1}, Lcom/fasterxml/jackson/databind/type/ClassKey;-><init>(Ljava/lang/Class;)V

    invoke-interface {v0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Class;

    :goto_0
    return-object p1
.end method

.method public final getActiveView()Ljava/lang/Class;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/lang/Class<",
            "*>;"
        }
    .end annotation

    .line 384
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_view:Ljava/lang/Class;

    return-object v0
.end method

.method public final getAttributes()Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;
    .locals 1

    .line 389
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_attributes:Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    return-object v0
.end method

.method public final getRootName()Ljava/lang/String;
    .locals 1

    .line 379
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_rootName:Ljava/lang/String;

    return-object v0
.end method

.method public final getSubtypeResolver()Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;
    .locals 1

    .line 375
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_subtypeResolver:Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;

    return-object v0
.end method

.method public final mixInCount()I
    .locals 1

    .line 408
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->_mixInAnnotations:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->size()I

    move-result v0

    :goto_0
    return v0
.end method

.method public abstract with(Lcom/fasterxml/jackson/core/Base64Variant;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/core/Base64Variant;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract with(Lcom/fasterxml/jackson/databind/AnnotationIntrospector;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/AnnotationIntrospector;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract with(Lcom/fasterxml/jackson/databind/PropertyNamingStrategy;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/PropertyNamingStrategy;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract with(Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract with(Lcom/fasterxml/jackson/databind/cfg/HandlerInstantiator;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/cfg/HandlerInstantiator;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract with(Lcom/fasterxml/jackson/databind/introspect/ClassIntrospector;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/introspect/ClassIntrospector;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract with(Lcom/fasterxml/jackson/databind/introspect/VisibilityChecker;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/introspect/VisibilityChecker<",
            "*>;)TT;"
        }
    .end annotation
.end method

.method public abstract with(Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/jsontype/SubtypeResolver;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract with(Lcom/fasterxml/jackson/databind/jsontype/TypeResolverBuilder;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/jsontype/TypeResolverBuilder<",
            "*>;)TT;"
        }
    .end annotation
.end method

.method public abstract with(Lcom/fasterxml/jackson/databind/type/TypeFactory;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/type/TypeFactory;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract with(Ljava/text/DateFormat;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/text/DateFormat;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract with(Ljava/util/Locale;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Locale;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract with(Ljava/util/TimeZone;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/TimeZone;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract withAppendedAnnotationIntrospector(Lcom/fasterxml/jackson/databind/AnnotationIntrospector;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/AnnotationIntrospector;",
            ")TT;"
        }
    .end annotation
.end method

.method public withAttribute(Ljava/lang/Object;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            ")TT;"
        }
    .end annotation

    .line 349
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->getAttributes()Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    move-result-object v0

    invoke-virtual {v0, p1, p2}, Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;->withSharedAttribute(Ljava/lang/Object;Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->with(Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;

    move-result-object p1

    return-object p1
.end method

.method public withAttributes(Ljava/util/Map;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/Object;",
            "Ljava/lang/Object;",
            ">;)TT;"
        }
    .end annotation

    .line 339
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->getAttributes()Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;->withSharedAttributes(Ljava/util/Map;)Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->with(Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;

    move-result-object p1

    return-object p1
.end method

.method public abstract withInsertedAnnotationIntrospector(Lcom/fasterxml/jackson/databind/AnnotationIntrospector;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/AnnotationIntrospector;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract withRootName(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")TT;"
        }
    .end annotation
.end method

.method public abstract withView(Ljava/lang/Class;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "*>;)TT;"
        }
    .end annotation
.end method

.method public abstract withVisibility(Lcom/fasterxml/jackson/annotation/PropertyAccessor;Lcom/fasterxml/jackson/annotation/JsonAutoDetect$Visibility;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/annotation/PropertyAccessor;",
            "Lcom/fasterxml/jackson/annotation/JsonAutoDetect$Visibility;",
            ")TT;"
        }
    .end annotation
.end method

.method public withoutAttribute(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            ")TT;"
        }
    .end annotation

    .line 359
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->getAttributes()Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;->withoutSharedAttribute(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;->with(Lcom/fasterxml/jackson/databind/cfg/ContextAttributes;)Lcom/fasterxml/jackson/databind/cfg/MapperConfigBase;

    move-result-object p1

    return-object p1
.end method
