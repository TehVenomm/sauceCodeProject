.class public Lcom/fasterxml/jackson/databind/MappingIterator;
.super Ljava/lang/Object;
.source "MappingIterator.java"

# interfaces
.implements Ljava/util/Iterator;
.implements Ljava/io/Closeable;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;",
        "Ljava/util/Iterator<",
        "TT;>;",
        "Ljava/io/Closeable;"
    }
.end annotation


# static fields
.field protected static final EMPTY_ITERATOR:Lcom/fasterxml/jackson/databind/MappingIterator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/fasterxml/jackson/databind/MappingIterator<",
            "*>;"
        }
    .end annotation
.end field


# instance fields
.field protected final _closeParser:Z

.field protected final _context:Lcom/fasterxml/jackson/databind/DeserializationContext;

.field protected final _deserializer:Lcom/fasterxml/jackson/databind/JsonDeserializer;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "TT;>;"
        }
    .end annotation
.end field

.field protected _hasNextChecked:Z

.field protected _parser:Lcom/fasterxml/jackson/core/JsonParser;

.field protected final _type:Lcom/fasterxml/jackson/databind/JavaType;

.field protected final _updatedValue:Ljava/lang/Object;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "TT;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 8

    .line 16
    new-instance v7, Lcom/fasterxml/jackson/databind/MappingIterator;

    const/4 v1, 0x0

    const/4 v2, 0x0

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    move-object v0, v7

    invoke-direct/range {v0 .. v6}, Lcom/fasterxml/jackson/databind/MappingIterator;-><init>(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/JsonDeserializer;ZLjava/lang/Object;)V

    sput-object v7, Lcom/fasterxml/jackson/databind/MappingIterator;->EMPTY_ITERATOR:Lcom/fasterxml/jackson/databind/MappingIterator;

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/JsonDeserializer;)V
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Lcom/fasterxml/jackson/core/JsonParser;",
            "Lcom/fasterxml/jackson/databind/DeserializationContext;",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;)V"
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    const/4 v5, 0x1

    const/4 v6, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v4, p4

    .line 52
    invoke-direct/range {v0 .. v6}, Lcom/fasterxml/jackson/databind/MappingIterator;-><init>(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/JsonDeserializer;ZLjava/lang/Object;)V

    return-void
.end method

.method protected constructor <init>(Lcom/fasterxml/jackson/databind/JavaType;Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Lcom/fasterxml/jackson/databind/JsonDeserializer;ZLjava/lang/Object;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/fasterxml/jackson/databind/JavaType;",
            "Lcom/fasterxml/jackson/core/JsonParser;",
            "Lcom/fasterxml/jackson/databind/DeserializationContext;",
            "Lcom/fasterxml/jackson/databind/JsonDeserializer<",
            "*>;Z",
            "Ljava/lang/Object;",
            ")V"
        }
    .end annotation

    .line 65
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 66
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_type:Lcom/fasterxml/jackson/databind/JavaType;

    .line 67
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    .line 68
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_context:Lcom/fasterxml/jackson/databind/DeserializationContext;

    .line 69
    iput-object p4, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_deserializer:Lcom/fasterxml/jackson/databind/JsonDeserializer;

    .line 70
    iput-boolean p5, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_closeParser:Z

    if-nez p6, :cond_0

    const/4 p1, 0x0

    .line 72
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_updatedValue:Ljava/lang/Object;

    goto :goto_0

    .line 74
    :cond_0
    iput-object p6, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_updatedValue:Ljava/lang/Object;

    :goto_0
    if-eqz p5, :cond_1

    if-eqz p2, :cond_1

    .line 87
    invoke-virtual {p2}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object p1

    sget-object p3, Lcom/fasterxml/jackson/core/JsonToken;->START_ARRAY:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne p1, p3, :cond_1

    .line 88
    invoke-virtual {p2}, Lcom/fasterxml/jackson/core/JsonParser;->clearCurrentToken()V

    :cond_1
    return-void
.end method

.method protected static emptyIterator()Lcom/fasterxml/jackson/databind/MappingIterator;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">()",
            "Lcom/fasterxml/jackson/databind/MappingIterator<",
            "TT;>;"
        }
    .end annotation

    .line 94
    sget-object v0, Lcom/fasterxml/jackson/databind/MappingIterator;->EMPTY_ITERATOR:Lcom/fasterxml/jackson/databind/MappingIterator;

    return-object v0
.end method


# virtual methods
.method protected _handleIOException(Ljava/io/IOException;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<R:",
            "Ljava/lang/Object;",
            ">(",
            "Ljava/io/IOException;",
            ")TR;"
        }
    .end annotation

    .line 295
    new-instance v0, Ljava/lang/RuntimeException;

    invoke-virtual {p1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1, p1}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v0
.end method

.method protected _handleMappingException(Lcom/fasterxml/jackson/databind/JsonMappingException;)Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<R:",
            "Ljava/lang/Object;",
            ">(",
            "Lcom/fasterxml/jackson/databind/JsonMappingException;",
            ")TR;"
        }
    .end annotation

    .line 291
    new-instance v0, Lcom/fasterxml/jackson/databind/RuntimeJsonMappingException;

    invoke-virtual {p1}, Lcom/fasterxml/jackson/databind/JsonMappingException;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v0, v1, p1}, Lcom/fasterxml/jackson/databind/RuntimeJsonMappingException;-><init>(Ljava/lang/String;Lcom/fasterxml/jackson/databind/JsonMappingException;)V

    throw v0
.end method

.method protected _throwNoSuchElement()Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<R:",
            "Ljava/lang/Object;",
            ">()TR;"
        }
    .end annotation

    .line 287
    new-instance v0, Ljava/util/NoSuchElementException;

    invoke-direct {v0}, Ljava/util/NoSuchElementException;-><init>()V

    throw v0
.end method

.method public close()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 134
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    if-eqz v0, :cond_0

    .line 135
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/JsonParser;->close()V

    :cond_0
    return-void
.end method

.method public getCurrentLocation()Lcom/fasterxml/jackson/core/JsonLocation;
    .locals 1

    .line 277
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentLocation()Lcom/fasterxml/jackson/core/JsonLocation;

    move-result-object v0

    return-object v0
.end method

.method public getParser()Lcom/fasterxml/jackson/core/JsonParser;
    .locals 1

    .line 252
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    return-object v0
.end method

.method public getParserSchema()Lcom/fasterxml/jackson/core/FormatSchema;
    .locals 1

    .line 263
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/JsonParser;->getSchema()Lcom/fasterxml/jackson/core/FormatSchema;

    move-result-object v0

    return-object v0
.end method

.method public hasNext()Z
    .locals 1

    .line 107
    :try_start_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/MappingIterator;->hasNextValue()Z

    move-result v0
    :try_end_0
    .catch Lcom/fasterxml/jackson/databind/JsonMappingException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return v0

    :catch_0
    move-exception v0

    .line 111
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/databind/MappingIterator;->_handleIOException(Ljava/io/IOException;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    return v0

    :catch_1
    move-exception v0

    .line 109
    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/databind/MappingIterator;->_handleMappingException(Lcom/fasterxml/jackson/databind/JsonMappingException;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    return v0
.end method

.method public hasNextValue()Z
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 151
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 154
    :cond_0
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_hasNextChecked:Z

    const/4 v2, 0x1

    if-nez v0, :cond_3

    .line 155
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/JsonParser;->getCurrentToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    .line 156
    iput-boolean v2, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_hasNextChecked:Z

    if-nez v0, :cond_3

    .line 158
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/JsonParser;->nextToken()Lcom/fasterxml/jackson/core/JsonToken;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 160
    sget-object v3, Lcom/fasterxml/jackson/core/JsonToken;->END_ARRAY:Lcom/fasterxml/jackson/core/JsonToken;

    if-ne v0, v3, :cond_3

    .line 161
    :cond_1
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    const/4 v2, 0x0

    .line 162
    iput-object v2, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    .line 163
    iget-boolean v2, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_closeParser:Z

    if-eqz v2, :cond_2

    .line 164
    invoke-virtual {v0}, Lcom/fasterxml/jackson/core/JsonParser;->close()V

    :cond_2
    return v1

    :cond_3
    return v2
.end method

.method public next()Ljava/lang/Object;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TT;"
        }
    .end annotation

    .line 119
    :try_start_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/MappingIterator;->nextValue()Ljava/lang/Object;

    move-result-object v0
    :try_end_0
    .catch Lcom/fasterxml/jackson/databind/JsonMappingException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    .line 123
    new-instance v1, Ljava/lang/RuntimeException;

    invoke-virtual {v0}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2, v0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1

    :catch_1
    move-exception v0

    .line 121
    new-instance v1, Lcom/fasterxml/jackson/databind/RuntimeJsonMappingException;

    invoke-virtual {v0}, Lcom/fasterxml/jackson/databind/JsonMappingException;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2, v0}, Lcom/fasterxml/jackson/databind/RuntimeJsonMappingException;-><init>(Ljava/lang/String;Lcom/fasterxml/jackson/databind/JsonMappingException;)V

    throw v1
.end method

.method public nextValue()Ljava/lang/Object;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TT;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 176
    iget-boolean v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_hasNextChecked:Z

    if-nez v0, :cond_0

    .line 177
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/MappingIterator;->hasNextValue()Z

    move-result v0

    if-nez v0, :cond_0

    .line 178
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/MappingIterator;->_throwNoSuchElement()Ljava/lang/Object;

    move-result-object v0

    return-object v0

    .line 181
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    if-nez v0, :cond_1

    .line 182
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/MappingIterator;->_throwNoSuchElement()Ljava/lang/Object;

    move-result-object v0

    return-object v0

    :cond_1
    const/4 v0, 0x0

    .line 184
    iput-boolean v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_hasNextChecked:Z

    .line 187
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_updatedValue:Ljava/lang/Object;

    if-nez v0, :cond_2

    .line 188
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_deserializer:Lcom/fasterxml/jackson/databind/JsonDeserializer;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_context:Lcom/fasterxml/jackson/databind/DeserializationContext;

    invoke-virtual {v0, v1, v2}, Lcom/fasterxml/jackson/databind/JsonDeserializer;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;)Ljava/lang/Object;

    move-result-object v0

    goto :goto_0

    .line 190
    :cond_2
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_deserializer:Lcom/fasterxml/jackson/databind/JsonDeserializer;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_context:Lcom/fasterxml/jackson/databind/DeserializationContext;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_updatedValue:Ljava/lang/Object;

    invoke-virtual {v0, v1, v2, v3}, Lcom/fasterxml/jackson/databind/JsonDeserializer;->deserialize(Lcom/fasterxml/jackson/core/JsonParser;Lcom/fasterxml/jackson/databind/DeserializationContext;Ljava/lang/Object;)Ljava/lang/Object;

    .line 191
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_updatedValue:Ljava/lang/Object;

    .line 194
    :goto_0
    iget-object v1, p0, Lcom/fasterxml/jackson/databind/MappingIterator;->_parser:Lcom/fasterxml/jackson/core/JsonParser;

    invoke-virtual {v1}, Lcom/fasterxml/jackson/core/JsonParser;->clearCurrentToken()V

    return-object v0
.end method

.method public readAll(Ljava/util/Collection;)Ljava/util/Collection;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<C::",
            "Ljava/util/Collection<",
            "-TT;>;>(TC;)TC;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 234
    :goto_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/MappingIterator;->hasNextValue()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 235
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/MappingIterator;->nextValue()Ljava/lang/Object;

    move-result-object v0

    invoke-interface {p1, v0}, Ljava/util/Collection;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    return-object p1
.end method

.method public readAll()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "TT;>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 207
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    invoke-virtual {p0, v0}, Lcom/fasterxml/jackson/databind/MappingIterator;->readAll(Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public readAll(Ljava/util/List;)Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<",
            "L::Ljava/util/List<",
            "-TT;>;>(T",
            "L;",
            ")T",
            "L;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 220
    :goto_0
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/MappingIterator;->hasNextValue()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 221
    invoke-virtual {p0}, Lcom/fasterxml/jackson/databind/MappingIterator;->nextValue()Ljava/lang/Object;

    move-result-object v0

    invoke-interface {p1, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_0
    return-object p1
.end method

.method public remove()V
    .locals 1

    .line 129
    new-instance v0, Ljava/lang/UnsupportedOperationException;

    invoke-direct {v0}, Ljava/lang/UnsupportedOperationException;-><init>()V

    throw v0
.end method
