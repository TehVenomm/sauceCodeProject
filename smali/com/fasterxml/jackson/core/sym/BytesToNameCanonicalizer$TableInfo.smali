.class final Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;
.super Ljava/lang/Object;
.source "BytesToNameCanonicalizer.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1a
    name = "TableInfo"
.end annotation


# instance fields
.field public final collCount:I

.field public final collEnd:I

.field public final collList:[Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;

.field public final count:I

.field public final longestCollisionList:I

.field public final mainHash:[I

.field public final mainHashMask:I

.field public final mainNames:[Lcom/fasterxml/jackson/core/sym/Name;


# direct methods
.method public constructor <init>(II[I[Lcom/fasterxml/jackson/core/sym/Name;[Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;III)V
    .locals 0

    .line 1170
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1171
    iput p1, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->count:I

    .line 1172
    iput p2, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->mainHashMask:I

    .line 1173
    iput-object p3, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->mainHash:[I

    .line 1174
    iput-object p4, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->mainNames:[Lcom/fasterxml/jackson/core/sym/Name;

    .line 1175
    iput-object p5, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->collList:[Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;

    .line 1176
    iput p6, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->collCount:I

    .line 1177
    iput p7, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->collEnd:I

    .line 1178
    iput p8, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->longestCollisionList:I

    return-void
.end method

.method public constructor <init>(Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;)V
    .locals 1

    .line 1182
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1183
    iget v0, p1, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;->_count:I

    iput v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->count:I

    .line 1184
    iget v0, p1, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;->_hashMask:I

    iput v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->mainHashMask:I

    .line 1185
    iget-object v0, p1, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;->_hash:[I

    iput-object v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->mainHash:[I

    .line 1186
    iget-object v0, p1, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;->_mainNames:[Lcom/fasterxml/jackson/core/sym/Name;

    iput-object v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->mainNames:[Lcom/fasterxml/jackson/core/sym/Name;

    .line 1187
    iget-object v0, p1, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;->_collList:[Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;

    iput-object v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->collList:[Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$Bucket;

    .line 1188
    iget v0, p1, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;->_collCount:I

    iput v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->collCount:I

    .line 1189
    iget v0, p1, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;->_collEnd:I

    iput v0, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->collEnd:I

    .line 1190
    iget p1, p1, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer;->_longestCollisionList:I

    iput p1, p0, Lcom/fasterxml/jackson/core/sym/BytesToNameCanonicalizer$TableInfo;->longestCollisionList:I

    return-void
.end method
