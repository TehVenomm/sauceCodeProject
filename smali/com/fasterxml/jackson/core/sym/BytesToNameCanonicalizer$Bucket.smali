.class final Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;
.super Ljava/lang/Object;
.source "BytesToNameCanonicalizer.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1a
    name = "Bucket"
.end annotation


# instance fields
.field public final hash:I

.field public final length:I

.field public final name:Lcom/fasterxml/jackson/core/sym/Name;

.field public final next:Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;


# direct methods
.method constructor <init>(Lcom/fasterxml/jackson/core/sym/Name;Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;)V
    .locals 1

    .line 1201
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1202
    iput-object p1, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->name:Lcom/fasterxml/jackson/core/sym/Name;

    .line 1203
    iput-object p2, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->next:Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;

    const/4 v0, 0x1

    if-nez p2, :cond_0

    goto :goto_0

    .line 1204
    :cond_0
    iget p2, p2, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->length:I

    add-int/2addr v0, p2

    :goto_0
    iput v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->length:I

    .line 1205
    invoke-virtual {p1}, Lcom/fasterxml/jackson/core/sym/Name;->hashCode()I

    move-result p1

    iput p1, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->hash:I

    return-void
.end method


# virtual methods
.method public find(III)Lcom/fasterxml/jackson/core/sym/Name;
    .locals 3

    .line 1209
    iget v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->hash:I

    if-ne v0, p1, :cond_0

    .line 1210
    iget-object v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->name:Lcom/fasterxml/jackson/core/sym/Name;

    invoke-virtual {v0, p2, p3}, Lcom/fasterxml/jackson/core/sym/Name;->equals(II)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1211
    iget-object p1, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->name:Lcom/fasterxml/jackson/core/sym/Name;

    return-object p1

    .line 1214
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->next:Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;

    :goto_0
    if-eqz v0, :cond_2

    .line 1215
    iget v1, v0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->hash:I

    if-ne v1, p1, :cond_1

    .line 1216
    iget-object v1, v0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->name:Lcom/fasterxml/jackson/core/sym/Name;

    .line 1217
    invoke-virtual {v1, p2, p3}, Lcom/fasterxml/jackson/core/sym/Name;->equals(II)Z

    move-result v2

    if-eqz v2, :cond_1

    return-object v1

    .line 1214
    :cond_1
    iget-object v0, v0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->next:Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;

    goto :goto_0

    :cond_2
    const/4 p1, 0x0

    return-object p1
.end method

.method public find(I[II)Lcom/fasterxml/jackson/core/sym/Name;
    .locals 3

    .line 1226
    iget v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->hash:I

    if-ne v0, p1, :cond_0

    .line 1227
    iget-object v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->name:Lcom/fasterxml/jackson/core/sym/Name;

    invoke-virtual {v0, p2, p3}, Lcom/fasterxml/jackson/core/sym/Name;->equals([II)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 1228
    iget-object p1, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->name:Lcom/fasterxml/jackson/core/sym/Name;

    return-object p1

    .line 1231
    :cond_0
    iget-object v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->next:Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;

    :goto_0
    if-eqz v0, :cond_2

    .line 1232
    iget v1, v0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->hash:I

    if-ne v1, p1, :cond_1

    .line 1233
    iget-object v1, v0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->name:Lcom/fasterxml/jackson/core/sym/Name;

    .line 1234
    invoke-virtual {v1, p2, p3}, Lcom/fasterxml/jackson/core/sym/Name;->equals([II)Z

    move-result v2

    if-eqz v2, :cond_1

    return-object v1

    .line 1231
    :cond_1
    iget-object v0, v0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;->next:Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;

    goto :goto_0

    :cond_2
    const/4 p1, 0x0

    return-object p1
.end method
