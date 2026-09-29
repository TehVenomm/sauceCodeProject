.class Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;
.super Ljava/lang/Object;
.source "ModelCache.java"

# interfaces
.implements Landroid/os/Parcelable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/github/droidfu/cachefu/ModelCache;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "DescribedCachedModel"
.end annotation


# instance fields
.field private cachedModel:Lcom/github/droidfu/cachefu/CachedModel;


# direct methods
.method constructor <init>()V
    .locals 0

    .line 111
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public describeContents()I
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public getCachedModel()Lcom/github/droidfu/cachefu/CachedModel;
    .locals 1

    .line 120
    iget-object v0, p0, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;->cachedModel:Lcom/github/droidfu/cachefu/CachedModel;

    return-object v0
.end method

.method public readFromParcel(Landroid/os/Parcel;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 136
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    .line 139
    :try_start_0
    invoke-static {v0}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object v0

    .line 140
    invoke-virtual {v0}, Ljava/lang/Class;->getClassLoader()Ljava/lang/ClassLoader;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/os/Parcel;->readParcelable(Ljava/lang/ClassLoader;)Landroid/os/Parcelable;

    move-result-object p1

    check-cast p1, Lcom/github/droidfu/cachefu/CachedModel;

    iput-object p1, p0, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;->cachedModel:Lcom/github/droidfu/cachefu/CachedModel;
    :try_end_0
    .catch Ljava/lang/ClassNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception p1

    .line 142
    new-instance v0, Ljava/io/IOException;

    invoke-virtual {p1}, Ljava/lang/ClassNotFoundException;->getMessage()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public setCachedModel(Lcom/github/droidfu/cachefu/CachedModel;)V
    .locals 0

    .line 116
    iput-object p1, p0, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;->cachedModel:Lcom/github/droidfu/cachefu/CachedModel;

    return-void
.end method

.method public writeToParcel(Landroid/os/Parcel;I)V
    .locals 1

    .line 130
    iget-object v0, p0, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;->cachedModel:Lcom/github/droidfu/cachefu/CachedModel;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 131
    iget-object v0, p0, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;->cachedModel:Lcom/github/droidfu/cachefu/CachedModel;

    invoke-virtual {p1, v0, p2}, Landroid/os/Parcel;->writeParcelable(Landroid/os/Parcelable;I)V

    .line 132
    iget-object v0, p0, Lcom/github/droidfu/cachefu/ModelCache$DescribedCachedModel;->cachedModel:Lcom/github/droidfu/cachefu/CachedModel;

    invoke-virtual {v0, p1, p2}, Lcom/github/droidfu/cachefu/CachedModel;->writeToParcel(Landroid/os/Parcel;I)V

    return-void
.end method
