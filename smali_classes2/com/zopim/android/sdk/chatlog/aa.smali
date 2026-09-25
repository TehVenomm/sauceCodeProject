.class Lcom/zopim/android/sdk/chatlog/aa;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Comparable;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/chatlog/aa$a;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Lcom/zopim/android/sdk/chatlog/aa;",
        ">",
        "Ljava/lang/Object;",
        "Ljava/lang/Comparable<",
        "Lcom/zopim/android/sdk/chatlog/aa;",
        ">;"
    }
.end annotation


# instance fields
.field public g:Ljava/lang/String;

.field public h:Lcom/zopim/android/sdk/chatlog/aa$a;

.field public i:Ljava/lang/String;

.field public j:Ljava/lang/String;

.field public k:Ljava/lang/String;

.field public l:Ljava/lang/Long;


# direct methods
.method constructor <init>()V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    sget-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->a:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    return-void
.end method

.method constructor <init>(Lcom/zopim/android/sdk/chatlog/aa;)V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    sget-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->a:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aa;->l:Ljava/lang/Long;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/aa;->l:Ljava/lang/Long;

    return-void
.end method


# virtual methods
.method public a(Lcom/zopim/android/sdk/chatlog/aa;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;)V"
        }
    .end annotation

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aa;->l:Ljava/lang/Long;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/aa;->l:Ljava/lang/Long;

    return-void
.end method

.method public b(Lcom/zopim/android/sdk/chatlog/aa;)I
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->l:Ljava/lang/Long;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aa;->l:Ljava/lang/Long;

    invoke-virtual {v0, p1}, Ljava/lang/Long;->compareTo(Ljava/lang/Long;)I

    move-result p1

    return p1
.end method

.method public synthetic compareTo(Ljava/lang/Object;)I
    .locals 0

    check-cast p1, Lcom/zopim/android/sdk/chatlog/aa;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/chatlog/aa;->b(Lcom/zopim/android/sdk/chatlog/aa;)I

    move-result p1

    return p1
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 4

    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    :cond_0
    const/4 v1, 0x0

    if-eqz p1, :cond_c

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    if-eq v2, v3, :cond_1

    goto :goto_5

    :cond_1
    check-cast p1, Lcom/zopim/android/sdk/chatlog/aa;

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    iget-object v3, p1, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_3

    goto :goto_0

    :cond_2
    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    if-eqz v2, :cond_3

    :goto_0
    return v1

    :cond_3
    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    iget-object v3, p1, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    if-eq v2, v3, :cond_4

    return v1

    :cond_4
    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    if-eqz v2, :cond_5

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    iget-object v3, p1, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_6

    goto :goto_1

    :cond_5
    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    if-eqz v2, :cond_6

    :goto_1
    return v1

    :cond_6
    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    if-eqz v2, :cond_7

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    iget-object v3, p1, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_8

    goto :goto_2

    :cond_7
    iget-object v2, p1, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    if-eqz v2, :cond_8

    :goto_2
    return v1

    :cond_8
    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    if-eqz v2, :cond_9

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    invoke-virtual {v2, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_b

    goto :goto_3

    :cond_9
    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    if-nez p1, :cond_a

    goto :goto_4

    :cond_a
    :goto_3
    const/4 v0, 0x0

    :cond_b
    :goto_4
    return v0

    :cond_c
    :goto_5
    return v1
.end method

.method public hashCode()I
    .locals 3

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->hashCode()I

    move-result v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    mul-int/lit8 v0, v0, 0x1f

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    if-eqz v2, :cond_1

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-virtual {v2}, Lcom/zopim/android/sdk/chatlog/aa$a;->hashCode()I

    move-result v2

    goto :goto_1

    :cond_1
    const/4 v2, 0x0

    :goto_1
    add-int/2addr v0, v2

    mul-int/lit8 v0, v0, 0x1f

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    if-eqz v2, :cond_2

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->hashCode()I

    move-result v2

    goto :goto_2

    :cond_2
    const/4 v2, 0x0

    :goto_2
    add-int/2addr v0, v2

    mul-int/lit8 v0, v0, 0x1f

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    if-eqz v2, :cond_3

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->hashCode()I

    move-result v2

    goto :goto_3

    :cond_3
    const/4 v2, 0x0

    :goto_3
    add-int/2addr v0, v2

    mul-int/lit8 v0, v0, 0x1f

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    if-eqz v2, :cond_4

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->hashCode()I

    move-result v1

    :cond_4
    add-int/2addr v0, v1

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, " type:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, " dispName:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " nick:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " id:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " ts:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/aa;->l:Ljava/lang/Long;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
