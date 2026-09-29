.class public Lcom/github/droidfu/cachefu/CachedList;
.super Lcom/github/droidfu/cachefu/CachedModel;
.source "CachedList.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<CO:",
        "Lcom/github/droidfu/cachefu/CachedModel;",
        ">",
        "Lcom/github/droidfu/cachefu/CachedModel;"
    }
.end annotation


# static fields
.field public static final CREATOR:Landroid/os/Parcelable$Creator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/os/Parcelable$Creator<",
            "Lcom/github/droidfu/cachefu/CachedList<",
            "Lcom/github/droidfu/cachefu/CachedModel;",
            ">;>;"
        }
    .end annotation
.end field


# instance fields
.field protected clazz:Ljava/lang/Class;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/Class<",
            "+",
            "Lcom/github/droidfu/cachefu/CachedModel;",
            ">;"
        }
    .end annotation
.end field

.field protected list:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "TCO;>;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 80
    new-instance v0, Lcom/github/droidfu/cachefu/CachedList$1;

    invoke-direct {v0}, Lcom/github/droidfu/cachefu/CachedList$1;-><init>()V

    sput-object v0, Lcom/github/droidfu/cachefu/CachedList;->CREATOR:Landroid/os/Parcelable$Creator;

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 14
    invoke-direct {p0}, Lcom/github/droidfu/cachefu/CachedModel;-><init>()V

    .line 15
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    return-void
.end method

.method public constructor <init>(Landroid/os/Parcel;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 29
    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/CachedModel;-><init>(Landroid/os/Parcel;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/Class;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "+",
            "Lcom/github/droidfu/cachefu/CachedModel;",
            ">;)V"
        }
    .end annotation

    .line 18
    invoke-direct {p0}, Lcom/github/droidfu/cachefu/CachedModel;-><init>()V

    .line 19
    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/CachedList;->initList(Ljava/lang/Class;)V

    .line 20
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    iput-object p1, p0, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    return-void
.end method

.method public constructor <init>(Ljava/lang/Class;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "+",
            "Lcom/github/droidfu/cachefu/CachedModel;",
            ">;I)V"
        }
    .end annotation

    .line 23
    invoke-direct {p0}, Lcom/github/droidfu/cachefu/CachedModel;-><init>()V

    .line 24
    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/CachedList;->initList(Ljava/lang/Class;)V

    .line 25
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1, p2}, Ljava/util/ArrayList;-><init>(I)V

    iput-object p1, p0, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    return-void
.end method

.method public constructor <init>(Ljava/lang/Class;Ljava/lang/String;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "+",
            "Lcom/github/droidfu/cachefu/CachedModel;",
            ">;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .line 33
    invoke-direct {p0, p2}, Lcom/github/droidfu/cachefu/CachedModel;-><init>(Ljava/lang/String;)V

    .line 34
    invoke-direct {p0, p1}, Lcom/github/droidfu/cachefu/CachedList;->initList(Ljava/lang/Class;)V

    .line 35
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    iput-object p1, p0, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    return-void
.end method

.method private initList(Ljava/lang/Class;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Class<",
            "+",
            "Lcom/github/droidfu/cachefu/CachedModel;",
            ">;)V"
        }
    .end annotation

    .line 39
    iput-object p1, p0, Lcom/github/droidfu/cachefu/CachedList;->clazz:Ljava/lang/Class;

    return-void
.end method


# virtual methods
.method public createKey(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    .line 58
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "list_"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 3

    .line 48
    instance-of v0, p1, Lcom/github/droidfu/cachefu/CachedList;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 52
    :cond_0
    check-cast p1, Lcom/github/droidfu/cachefu/CachedList;

    .line 53
    iget-object v0, p0, Lcom/github/droidfu/cachefu/CachedList;->clazz:Ljava/lang/Class;

    iget-object v2, p1, Lcom/github/droidfu/cachefu/CachedList;->clazz:Ljava/lang/Class;

    invoke-virtual {v0, v2}, Ljava/lang/Object;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    iget-object p1, p1, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    invoke-virtual {v0, p1}, Ljava/util/ArrayList;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    const/4 v1, 0x1

    :cond_1
    return v1
.end method

.method public getList()Ljava/util/ArrayList;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/ArrayList<",
            "TCO;>;"
        }
    .end annotation

    .line 43
    iget-object v0, p0, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    return-object v0
.end method

.method public readFromParcel(Landroid/os/Parcel;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 104
    invoke-super {p0, p1}, Lcom/github/droidfu/cachefu/CachedModel;->readFromParcel(Landroid/os/Parcel;)V

    .line 105
    invoke-virtual {p1}, Landroid/os/Parcel;->readString()Ljava/lang/String;

    move-result-object v0

    .line 107
    :try_start_0
    invoke-static {v0}, Ljava/lang/Class;->forName(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object v0

    iput-object v0, p0, Lcom/github/droidfu/cachefu/CachedList;->clazz:Ljava/lang/Class;

    .line 108
    iget-object v0, p0, Lcom/github/droidfu/cachefu/CachedList;->clazz:Ljava/lang/Class;

    const-string v1, "CREATOR"

    invoke-virtual {v0, v1}, Ljava/lang/Class;->getField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v0

    invoke-virtual {v0, p0}, Ljava/lang/reflect/Field;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/os/Parcelable$Creator;

    invoke-virtual {p1, v0}, Landroid/os/Parcel;->createTypedArrayList(Landroid/os/Parcelable$Creator;)Ljava/util/ArrayList;

    move-result-object p1

    iput-object p1, p0, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 110
    invoke-virtual {p1}, Ljava/lang/Exception;->printStackTrace()V

    :goto_0
    return-void
.end method

.method public reloadAll(Lcom/github/droidfu/cachefu/ModelCache;)Z
    .locals 3

    .line 62
    invoke-virtual {p0, p1}, Lcom/github/droidfu/cachefu/CachedList;->reload(Lcom/github/droidfu/cachefu/ModelCache;)Z

    move-result v0

    .line 63
    iget-object v1, p0, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    invoke-virtual {v1}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/github/droidfu/cachefu/CachedModel;

    .line 64
    invoke-virtual {v2, p1}, Lcom/github/droidfu/cachefu/CachedModel;->reload(Lcom/github/droidfu/cachefu/ModelCache;)Z

    move-result v2

    if-eqz v2, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_1
    return v0
.end method

.method public reloadFromCachedModel(Lcom/github/droidfu/cachefu/ModelCache;Lcom/github/droidfu/cachefu/CachedModel;)Z
    .locals 0

    .line 74
    check-cast p2, Lcom/github/droidfu/cachefu/CachedList;

    .line 75
    iget-object p1, p2, Lcom/github/droidfu/cachefu/CachedList;->clazz:Ljava/lang/Class;

    iput-object p1, p0, Lcom/github/droidfu/cachefu/CachedList;->clazz:Ljava/lang/Class;

    .line 76
    iget-object p1, p2, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    iput-object p1, p0, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    const/4 p1, 0x0

    return p1
.end method

.method public writeToParcel(Landroid/os/Parcel;I)V
    .locals 0

    .line 116
    invoke-super {p0, p1, p2}, Lcom/github/droidfu/cachefu/CachedModel;->writeToParcel(Landroid/os/Parcel;I)V

    .line 117
    iget-object p2, p0, Lcom/github/droidfu/cachefu/CachedList;->clazz:Ljava/lang/Class;

    invoke-virtual {p2}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeString(Ljava/lang/String;)V

    .line 118
    iget-object p2, p0, Lcom/github/droidfu/cachefu/CachedList;->list:Ljava/util/ArrayList;

    invoke-virtual {p1, p2}, Landroid/os/Parcel;->writeTypedList(Ljava/util/List;)V

    return-void
.end method
