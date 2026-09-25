.class public final Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;
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
    name = "GeneratorSettings"
.end annotation


# static fields
.field public static final empty:Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;

.field private static final serialVersionUID:J = 0x1L


# instance fields
.field public final characterEscapes:Lcom/fasterxml/jackson/core/io/CharacterEscapes;

.field public final prettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

.field public final rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

.field public final schema:Lcom/fasterxml/jackson/core/FormatSchema;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 1248
    new-instance v0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;

    const/4 v1, 0x0

    invoke-direct {v0, v1, v1, v1, v1}, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;-><init>(Lcom/fasterxml/jackson/core/PrettyPrinter;Lcom/fasterxml/jackson/core/FormatSchema;Lcom/fasterxml/jackson/core/io/CharacterEscapes;Lcom/fasterxml/jackson/core/SerializableString;)V

    sput-object v0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->empty:Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;

    return-void
.end method

.method public constructor <init>(Lcom/fasterxml/jackson/core/PrettyPrinter;Lcom/fasterxml/jackson/core/FormatSchema;Lcom/fasterxml/jackson/core/io/CharacterEscapes;Lcom/fasterxml/jackson/core/SerializableString;)V
    .locals 0

    .line 1278
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1279
    iput-object p1, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->prettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    .line 1280
    iput-object p2, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->schema:Lcom/fasterxml/jackson/core/FormatSchema;

    .line 1281
    iput-object p3, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->characterEscapes:Lcom/fasterxml/jackson/core/io/CharacterEscapes;

    .line 1282
    iput-object p4, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    return-void
.end method


# virtual methods
.method public with(Lcom/fasterxml/jackson/core/FormatSchema;)Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;
    .locals 4

    .line 1295
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->schema:Lcom/fasterxml/jackson/core/FormatSchema;

    if-ne v0, p1, :cond_0

    move-object v0, p0

    goto :goto_0

    :cond_0
    new-instance v0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->prettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->characterEscapes:Lcom/fasterxml/jackson/core/io/CharacterEscapes;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    invoke-direct {v0, v1, p1, v2, v3}, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;-><init>(Lcom/fasterxml/jackson/core/PrettyPrinter;Lcom/fasterxml/jackson/core/FormatSchema;Lcom/fasterxml/jackson/core/io/CharacterEscapes;Lcom/fasterxml/jackson/core/SerializableString;)V

    :goto_0
    return-object v0
.end method

.method public with(Lcom/fasterxml/jackson/core/PrettyPrinter;)Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;
    .locals 4

    if-nez p1, :cond_0

    .line 1288
    sget-object p1, Lcom/fasterxml/jackson/databind/ObjectWriter;->NULL_PRETTY_PRINTER:Lcom/fasterxml/jackson/core/PrettyPrinter;

    .line 1290
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->prettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    if-ne p1, v0, :cond_1

    move-object v0, p0

    goto :goto_0

    :cond_1
    new-instance v0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->schema:Lcom/fasterxml/jackson/core/FormatSchema;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->characterEscapes:Lcom/fasterxml/jackson/core/io/CharacterEscapes;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    invoke-direct {v0, p1, v1, v2, v3}, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;-><init>(Lcom/fasterxml/jackson/core/PrettyPrinter;Lcom/fasterxml/jackson/core/FormatSchema;Lcom/fasterxml/jackson/core/io/CharacterEscapes;Lcom/fasterxml/jackson/core/SerializableString;)V

    :goto_0
    return-object v0
.end method

.method public with(Lcom/fasterxml/jackson/core/io/CharacterEscapes;)Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;
    .locals 4

    .line 1300
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->characterEscapes:Lcom/fasterxml/jackson/core/io/CharacterEscapes;

    if-ne v0, p1, :cond_0

    move-object v0, p0

    goto :goto_0

    :cond_0
    new-instance v0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->prettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->schema:Lcom/fasterxml/jackson/core/FormatSchema;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    invoke-direct {v0, v1, v2, p1, v3}, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;-><init>(Lcom/fasterxml/jackson/core/PrettyPrinter;Lcom/fasterxml/jackson/core/FormatSchema;Lcom/fasterxml/jackson/core/io/CharacterEscapes;Lcom/fasterxml/jackson/core/SerializableString;)V

    :goto_0
    return-object v0
.end method

.method public withRootValueSeparator(Lcom/fasterxml/jackson/core/SerializableString;)Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;
    .locals 4

    if-nez p1, :cond_0

    .line 1318
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    if-nez v0, :cond_1

    return-object p0

    .line 1322
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    if-eqz v0, :cond_1

    invoke-interface {p1}, Lcom/fasterxml/jackson/core/SerializableString;->getValue()Ljava/lang/String;

    move-result-object v0

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    invoke-interface {v1}, Lcom/fasterxml/jackson/core/SerializableString;->getValue()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    return-object p0

    .line 1327
    :cond_1
    new-instance v0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->prettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->schema:Lcom/fasterxml/jackson/core/FormatSchema;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->characterEscapes:Lcom/fasterxml/jackson/core/io/CharacterEscapes;

    invoke-direct {v0, v1, v2, v3, p1}, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;-><init>(Lcom/fasterxml/jackson/core/PrettyPrinter;Lcom/fasterxml/jackson/core/FormatSchema;Lcom/fasterxml/jackson/core/io/CharacterEscapes;Lcom/fasterxml/jackson/core/SerializableString;)V

    return-object v0
.end method

.method public withRootValueSeparator(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;
    .locals 5

    if-nez p1, :cond_0

    .line 1306
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    if-nez v0, :cond_1

    return-object p0

    .line 1309
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->rootValueSeparator:Lcom/fasterxml/jackson/core/SerializableString;

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    return-object p0

    .line 1312
    :cond_1
    new-instance v0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;

    iget-object v1, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->prettyPrinter:Lcom/fasterxml/jackson/core/PrettyPrinter;

    iget-object v2, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->schema:Lcom/fasterxml/jackson/core/FormatSchema;

    iget-object v3, p0, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;->characterEscapes:Lcom/fasterxml/jackson/core/io/CharacterEscapes;

    if-nez p1, :cond_2

    const/4 p1, 0x0

    goto :goto_0

    :cond_2
    new-instance v4, Lcom/fasterxml/jackson/core/io/SerializedString;

    invoke-direct {v4, p1}, Lcom/fasterxml/jackson/core/io/SerializedString;-><init>(Ljava/lang/String;)V

    move-object p1, v4

    :goto_0
    invoke-direct {v0, v1, v2, v3, p1}, Lcom/fasterxml/jackson/databind/ObjectWriter$GeneratorSettings;-><init>(Lcom/fasterxml/jackson/core/PrettyPrinter;Lcom/fasterxml/jackson/core/FormatSchema;Lcom/fasterxml/jackson/core/io/CharacterEscapes;Lcom/fasterxml/jackson/core/SerializableString;)V

    return-object v0
.end method
