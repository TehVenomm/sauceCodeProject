.class final Lcom/github/droidfu/cachefu/CachedList$1;
.super Ljava/lang/Object;
.source "CachedList.java"

# interfaces
.implements Landroid/os/Parcelable$Creator;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/github/droidfu/cachefu/CachedList;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Landroid/os/Parcelable$Creator<",
        "Lcom/github/droidfu/cachefu/CachedList<",
        "Lcom/github/droidfu/cachefu/CachedModel;",
        ">;>;"
    }
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 80
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public createFromParcel(Landroid/os/Parcel;)Lcom/github/droidfu/cachefu/CachedList;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/os/Parcel;",
            ")",
            "Lcom/github/droidfu/cachefu/CachedList<",
            "Lcom/github/droidfu/cachefu/CachedModel;",
            ">;"
        }
    .end annotation

    .line 86
    :try_start_0
    new-instance v0, Lcom/github/droidfu/cachefu/CachedList;

    invoke-direct {v0, p1}, Lcom/github/droidfu/cachefu/CachedList;-><init>(Landroid/os/Parcel;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception p1

    .line 88
    invoke-virtual {p1}, Ljava/io/IOException;->printStackTrace()V

    const/4 p1, 0x0

    return-object p1
.end method

.method public bridge synthetic createFromParcel(Landroid/os/Parcel;)Ljava/lang/Object;
    .locals 0

    .line 80
    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/CachedList$1;->createFromParcel(Landroid/os/Parcel;)Lcom/github/droidfu/cachefu/CachedList;

    move-result-object p1

    return-object p1
.end method

.method public newArray(I)[Lcom/github/droidfu/cachefu/CachedList;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)[",
            "Lcom/github/droidfu/cachefu/CachedList<",
            "Lcom/github/droidfu/cachefu/CachedModel;",
            ">;"
        }
    .end annotation

    .line 96
    new-array p1, p1, [Lcom/github/droidfu/cachefu/CachedList;

    return-object p1
.end method

.method public bridge synthetic newArray(I)[Ljava/lang/Object;
    .locals 0

    .line 80
    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/CachedList$1;->newArray(I)[Lcom/github/droidfu/cachefu/CachedList;

    move-result-object p1

    return-object p1
.end method
