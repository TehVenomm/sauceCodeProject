.class final Lcom/zopim/android/sdk/chatlog/g;
.super Lcom/zopim/android/sdk/chatlog/aa;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/chatlog/aa<",
        "Lcom/zopim/android/sdk/chatlog/g;",
        ">;"
    }
.end annotation


# instance fields
.field public a:Ljava/lang/String;

.field public b:Z


# direct methods
.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/aa;-><init>()V

    sget-object v0, Lcom/zopim/android/sdk/chatlog/aa$a;->d:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    return-void
.end method


# virtual methods
.method public bridge synthetic a(Lcom/zopim/android/sdk/chatlog/aa;)V
    .locals 0

    check-cast p1, Lcom/zopim/android/sdk/chatlog/g;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/chatlog/g;->a(Lcom/zopim/android/sdk/chatlog/g;)V

    return-void
.end method

.method public a(Lcom/zopim/android/sdk/chatlog/g;)V
    .locals 1

    invoke-super {p0, p1}, Lcom/zopim/android/sdk/chatlog/aa;->a(Lcom/zopim/android/sdk/chatlog/aa;)V

    iget-object v0, p1, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    iget-boolean p1, p1, Lcom/zopim/android/sdk/chatlog/g;->b:Z

    iput-boolean p1, p0, Lcom/zopim/android/sdk/chatlog/g;->b:Z

    return-void
.end method

.method public equals(Ljava/lang/Object;)Z
    .locals 4

    const/4 v0, 0x1

    if-ne p0, p1, :cond_0

    return v0

    :cond_0
    const/4 v1, 0x0

    if-eqz p1, :cond_7

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v2

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    if-eq v2, v3, :cond_1

    goto :goto_2

    :cond_1
    invoke-super {p0, p1}, Lcom/zopim/android/sdk/chatlog/aa;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_2

    return v1

    :cond_2
    check-cast p1, Lcom/zopim/android/sdk/chatlog/g;

    iget-boolean v2, p0, Lcom/zopim/android/sdk/chatlog/g;->b:Z

    iget-boolean v3, p1, Lcom/zopim/android/sdk/chatlog/g;->b:Z

    if-eq v2, v3, :cond_3

    return v1

    :cond_3
    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    if-eqz v2, :cond_4

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    invoke-virtual {v2, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_6

    goto :goto_0

    :cond_4
    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    if-nez p1, :cond_5

    goto :goto_1

    :cond_5
    :goto_0
    const/4 v0, 0x0

    :cond_6
    :goto_1
    return v0

    :cond_7
    :goto_2
    return v1
.end method

.method public hashCode()I
    .locals 2

    invoke-super {p0}, Lcom/zopim/android/sdk/chatlog/aa;->hashCode()I

    move-result v0

    mul-int/lit8 v0, v0, 0x1f

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->hashCode()I

    move-result v1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    add-int/2addr v0, v1

    mul-int/lit8 v0, v0, 0x1f

    iget-boolean v1, p0, Lcom/zopim/android/sdk/chatlog/g;->b:Z

    add-int/2addr v0, v1

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, " avatarUri:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/g;->a:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " typing:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/zopim/android/sdk/chatlog/g;->b:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-super {p0}, Lcom/zopim/android/sdk/chatlog/aa;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
