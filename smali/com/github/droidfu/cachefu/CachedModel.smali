.class public abstract Lcom/github/droidfu/cachefu/CachedModel;
.super Ljava/lang/Object;
.source "CachedModel.java"

# interfaces
.implements Landroid/os/Parcelable;


# instance fields
.field private id:Ljava/lang/String;

.field private transactionId:J


# direct methods
.method public constructor <init>()V
    .locals 2

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-wide/high16 v0, -0x8000000000000000L

    .line 11
    iput-wide v0, p0, Lcom/github/droidfu/cachefu/CachedModel;->transactionId:J

    return-void
.end method

.method public constructor <init>(Landroid/os/Parcel;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-wide/high16 v0, -0x8000000000000000L

    .line 11
    iput-wide v0, p0, Lcom/github/droidfu/cachefu/CachedModel;->transactionId:J

    .line 17
    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/CachedModel;->readFromParcel(Landroid/os/Parcel;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;)V
    .locals 2

    .line 20
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-wide/high16 v0, -0x8000000000000000L

    .line 11
    iput-wide v0, p0, Lcom/github/droidfu/cachefu/CachedModel;->transactionId:J

    .line 21
    iput-object p1, p0, Lcom/github/droidfu/cachefu/CachedModel;->id:Ljava/lang/String;

    return-void
.end method

.method public static find(Lcom/github/droidfu/cachefu/ModelCache;Ljava/lang/String;Ljava/lang/Class;)Lcom/github/droidfu/cachefu/CachedModel;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/github/droidfu/cachefu/ModelCache;",
            "Ljava/lang/String;",
            "Ljava/lang/Class<",
            "+",
            "Lcom/github/droidfu/cachefu/CachedModel;",
            ">;)",
            "Lcom/github/droidfu/cachefu/CachedModel;"
        }
    .end annotation

    const/4 v0, 0x0

    .line 48
    :try_start_0
    invoke-virtual {p2}, Ljava/lang/Class;->newInstance()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/github/droidfu/cachefu/CachedModel;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 52
    invoke-virtual {p2, p1}, Lcom/github/droidfu/cachefu/CachedModel;->setId(Ljava/lang/String;)V

    .line 53
    invoke-virtual {p2, p0}, Lcom/github/droidfu/cachefu/CachedModel;->reload(Lcom/github/droidfu/cachefu/ModelCache;)Z

    move-result p0

    if-eqz p0, :cond_0

    return-object p2

    :cond_0
    return-object v0

    :catch_0
    return-object v0
.end method


# virtual methods
.method public abstract createKey(Ljava/lang/String;)Ljava/lang/String;
.end method

.method public describeContents()I
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public getId()Ljava/lang/String;
    .locals 1

    .line 25
    iget-object v0, p0, Lcom/github/droidfu/cachefu/CachedModel;->id:Ljava/lang/String;

    return-object v0
.end method

.method public getKey()Ljava/lang/String;
    .locals 1

    .line 37
    iget-object v0, p0, Lcom/github/droidfu/cachefu/CachedModel;->id:Ljava/lang/String;

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 40
    :cond_0
    iget-object v0, p0, Lcom/github/droidfu/cachefu/CachedModel;->id:Ljava/lang/String;

    invoke-virtual {p0, v0}, Lcom/github/droidfu/cachefu/CachedModel;->createKey(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public readFromParcel(Landroid/os/Parcel;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 105
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/github/droidfu/cachefu/CachedModel;->id:Ljava/lang/String;

    .line 106
    invoke-virtual {p1}, Landroid/os/Parcel;->readLong()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/github/droidfu/cachefu/CachedModel;->transactionId:J

    return-void
.end method

.method public reload(Lcom/github/droidfu/cachefu/ModelCache;)Z
    .locals 7

    .line 74
    invoke-virtual {p0}, Lcom/github/droidfu/cachefu/CachedModel;->getKey()Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz p1, :cond_1

    if-eqz v0, :cond_1

    .line 76
    invoke-virtual {p1, v0}, Lcom/github/droidfu/cachefu/ModelCache;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/github/droidfu/cachefu/CachedModel;

    if-eqz v0, :cond_0

    .line 77
    iget-wide v2, v0, Lcom/github/droidfu/cachefu/CachedModel;->transactionId:J

    iget-wide v4, p0, Lcom/github/droidfu/cachefu/CachedModel;->transactionId:J

    cmp-long v6, v2, v4

    if-lez v6, :cond_0

    .line 78
    invoke-virtual {p0, p1, v0}, Lcom/github/droidfu/cachefu/CachedModel;->reloadFromCachedModel(Lcom/github/droidfu/cachefu/ModelCache;Lcom/github/droidfu/cachefu/CachedModel;)Z

    const/4 p1, 0x1

    return p1

    :cond_0
    return v1

    :cond_1
    return v1
.end method

.method public abstract reloadFromCachedModel(Lcom/github/droidfu/cachefu/ModelCache;Lcom/github/droidfu/cachefu/CachedModel;)Z
.end method

.method public save(Lcom/github/droidfu/cachefu/ModelCache;)Z
    .locals 1

    .line 61
    invoke-virtual {p0}, Lcom/github/droidfu/cachefu/CachedModel;->getKey()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, p1, v0}, Lcom/github/droidfu/cachefu/CachedModel;->save(Lcom/github/droidfu/cachefu/ModelCache;Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method protected save(Lcom/github/droidfu/cachefu/ModelCache;Ljava/lang/String;)Z
    .locals 0

    if-eqz p1, :cond_0

    if-eqz p2, :cond_0

    .line 66
    invoke-virtual {p1, p2, p0}, Lcom/github/droidfu/cachefu/ModelCache;->put(Ljava/lang/String;Lcom/github/droidfu/cachefu/CachedModel;)Lcom/github/droidfu/cachefu/CachedModel;

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method public setId(Ljava/lang/String;)V
    .locals 0

    .line 29
    iput-object p1, p0, Lcom/github/droidfu/cachefu/CachedModel;->id:Ljava/lang/String;

    return-void
.end method

.method setTransactionId(J)V
    .locals 0

    .line 33
    iput-wide p1, p0, Lcom/github/droidfu/cachefu/CachedModel;->transactionId:J

    return-void
.end method

.method public writeToParcel(Landroid/os/Parcel;I)V
    .locals 2

    .line 99
    iget-object p2, p0, Lcom/github/droidfu/cachefu/CachedModel;->id:Ljava/lang/String;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 100
    iget-wide v0, p0, Lcom/github/droidfu/cachefu/CachedModel;->transactionId:J

    invoke-virtual {p1, v0, v1}, Landroid/os/Parcel;->writeLong(J)V

    return-void
.end method
