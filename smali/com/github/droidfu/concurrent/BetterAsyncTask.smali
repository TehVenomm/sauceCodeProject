.class public abstract Lcom/github/droidfu/concurrent/BetterAsyncTask;
.super Landroid/os/AsyncTask;
.source "BetterAsyncTask.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "<ParameterT:",
        "Ljava/lang/Object;",
        "ProgressT:",
        "Ljava/lang/Object;",
        "ReturnT:",
        "Ljava/lang/Object;",
        ">",
        "Landroid/os/AsyncTask<",
        "TParameterT;TProgressT;TReturnT;>;"
    }
.end annotation


# instance fields
.field private final appContext:Lcom/github/droidfu/DroidFuApplication;

.field private callable:Lcom/github/droidfu/concurrent/BetterAsyncTaskCallable;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lcom/github/droidfu/concurrent/BetterAsyncTaskCallable<",
            "TParameterT;TProgressT;TReturnT;>;"
        }
    .end annotation
.end field

.field private final callerId:Ljava/lang/String;

.field private final contextIsDroidFuActivity:Z

.field private dialogId:I

.field private error:Ljava/lang/Exception;

.field private isTitleProgressEnabled:Z

.field private isTitleProgressIndeterminateEnabled:Z


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 3

    .line 72
    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    const/4 v0, 0x1

    .line 59
    iput-boolean v0, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->isTitleProgressIndeterminateEnabled:Z

    const/4 v1, 0x0

    .line 65
    iput v1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->dialogId:I

    .line 74
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    instance-of v1, v1, Lcom/github/droidfu/DroidFuApplication;

    if-eqz v1, :cond_2

    .line 78
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    check-cast v1, Lcom/github/droidfu/DroidFuApplication;

    iput-object v1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->appContext:Lcom/github/droidfu/DroidFuApplication;

    .line 79
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object v1

    iput-object v1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->callerId:Ljava/lang/String;

    .line 80
    instance-of v1, p1, Lcom/github/droidfu/activities/BetterActivity;

    iput-boolean v1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->contextIsDroidFuActivity:Z

    .line 82
    iget-object v1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->appContext:Lcom/github/droidfu/DroidFuApplication;

    iget-object v2, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->callerId:Ljava/lang/String;

    invoke-virtual {v1, v2, p1}, Lcom/github/droidfu/DroidFuApplication;->setActiveContext(Ljava/lang/String;Landroid/content/Context;)V

    .line 84
    iget-boolean v1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->contextIsDroidFuActivity:Z

    if-eqz v1, :cond_1

    .line 85
    check-cast p1, Lcom/github/droidfu/activities/BetterActivity;

    invoke-interface {p1}, Lcom/github/droidfu/activities/BetterActivity;->getWindowFeatures()I

    move-result p1

    and-int/lit8 v1, p1, 0x2

    const/4 v2, 0x2

    if-ne v2, v1, :cond_0

    .line 87
    iput-boolean v0, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->isTitleProgressEnabled:Z

    goto :goto_0

    :cond_0
    const/4 v1, 0x5

    and-int/2addr p1, v1

    if-ne v1, p1, :cond_1

    .line 89
    iput-boolean v0, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->isTitleProgressIndeterminateEnabled:Z

    :cond_1
    :goto_0
    return-void

    .line 75
    :cond_2
    new-instance p1, Ljava/lang/IllegalArgumentException;

    const-string v0, "context bound to this task must be a DroidFu context (DroidFuApplication)"

    invoke-direct {p1, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p1
.end method


# virtual methods
.method protected abstract after(Landroid/content/Context;Ljava/lang/Object;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "TReturnT;)V"
        }
    .end annotation
.end method

.method protected before(Landroid/content/Context;)V
    .locals 0

    return-void
.end method

.method public disableDialog()V
    .locals 1

    const/4 v0, -0x1

    .line 250
    iput v0, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->dialogId:I

    return-void
.end method

.method protected varargs doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "[TParameterT;)TReturnT;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 170
    iget-object p1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->callable:Lcom/github/droidfu/concurrent/BetterAsyncTaskCallable;

    if-eqz p1, :cond_0

    .line 171
    iget-object p1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->callable:Lcom/github/droidfu/concurrent/BetterAsyncTaskCallable;

    invoke-interface {p1, p0}, Lcom/github/droidfu/concurrent/BetterAsyncTaskCallable;->call(Lcom/github/droidfu/concurrent/BetterAsyncTask;)Ljava/lang/Object;

    move-result-object p1

    return-object p1

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method

.method protected final varargs doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "([TParameterT;)TReturnT;"
        }
    .end annotation

    .line 152
    invoke-virtual {p0}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->getCallingContext()Landroid/content/Context;

    move-result-object v0

    .line 154
    :try_start_0
    invoke-virtual {p0, v0, p1}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->doCheckedInBackground(Landroid/content/Context;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 156
    iput-object p1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->error:Ljava/lang/Exception;

    const/4 p1, 0x0

    :goto_0
    return-object p1
.end method

.method public failed()Z
    .locals 1

    .line 225
    iget-object v0, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->error:Ljava/lang/Exception;

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    return v0

    :cond_0
    const/4 v0, 0x0

    return v0
.end method

.method protected getCallingContext()Landroid/content/Context;
    .locals 4

    const/4 v0, 0x0

    .line 103
    :try_start_0
    iget-object v1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->appContext:Lcom/github/droidfu/DroidFuApplication;

    iget-object v2, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->callerId:Ljava/lang/String;

    invoke-virtual {v1, v2}, Lcom/github/droidfu/DroidFuApplication;->getActiveContext(Ljava/lang/String;)Landroid/content/Context;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 104
    iget-object v2, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->callerId:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Class;->getCanonicalName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_1

    .line 105
    instance-of v2, v1, Landroid/app/Activity;

    if-eqz v2, :cond_0

    move-object v2, v1

    check-cast v2, Landroid/app/Activity;

    invoke-virtual {v2}, Landroid/app/Activity;->isFinishing()Z

    move-result v2
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    if-eqz v2, :cond_0

    goto :goto_0

    :cond_0
    return-object v1

    :cond_1
    :goto_0
    return-object v0

    :catch_0
    move-exception v1

    .line 112
    invoke-virtual {v1}, Ljava/lang/Exception;->printStackTrace()V

    return-object v0
.end method

.method protected abstract handleError(Landroid/content/Context;Ljava/lang/Exception;)V
.end method

.method protected final onPostExecute(Ljava/lang/Object;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TReturnT;)V"
        }
    .end annotation

    .line 186
    invoke-virtual {p0}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->getCallingContext()Landroid/content/Context;

    move-result-object v0

    if-nez v0, :cond_0

    .line 188
    const-class p1, Lcom/github/droidfu/concurrent/BetterAsyncTask;

    invoke-virtual {p1}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object p1

    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "skipping post-exec handler for task "

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    .line 189
    invoke-virtual {p0}, Ljava/lang/Object;->hashCode()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, " (context is null)"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 188
    invoke-static {p1, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    .line 193
    :cond_0
    iget-boolean v1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->contextIsDroidFuActivity:Z

    if-eqz v1, :cond_3

    .line 194
    move-object v1, v0

    check-cast v1, Landroid/app/Activity;

    .line 195
    iget v2, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->dialogId:I

    const/4 v3, -0x1

    if-le v2, v3, :cond_1

    .line 196
    iget v2, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->dialogId:I

    invoke-virtual {v1, v2}, Landroid/app/Activity;->removeDialog(I)V

    .line 198
    :cond_1
    iget-boolean v2, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->isTitleProgressEnabled:Z

    const/4 v3, 0x0

    if-eqz v2, :cond_2

    .line 199
    invoke-virtual {v1, v3}, Landroid/app/Activity;->setProgressBarVisibility(Z)V

    goto :goto_0

    .line 200
    :cond_2
    iget-boolean v2, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->isTitleProgressIndeterminateEnabled:Z

    if-eqz v2, :cond_3

    .line 201
    invoke-virtual {v1, v3}, Landroid/app/Activity;->setProgressBarIndeterminateVisibility(Z)V

    .line 205
    :cond_3
    :goto_0
    invoke-virtual {p0}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->failed()Z

    move-result v1

    if-eqz v1, :cond_4

    .line 206
    iget-object p1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->error:Ljava/lang/Exception;

    invoke-virtual {p0, v0, p1}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->handleError(Landroid/content/Context;Ljava/lang/Exception;)V

    goto :goto_1

    .line 208
    :cond_4
    invoke-virtual {p0, v0, p1}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->after(Landroid/content/Context;Ljava/lang/Object;)V

    :goto_1
    return-void
.end method

.method protected final onPreExecute()V
    .locals 5

    .line 119
    invoke-virtual {p0}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->getCallingContext()Landroid/content/Context;

    move-result-object v0

    const/4 v1, 0x1

    if-nez v0, :cond_0

    .line 121
    const-class v0, Lcom/github/droidfu/concurrent/BetterAsyncTask;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    new-instance v2, Ljava/lang/StringBuilder;

    const-string v3, "skipping pre-exec handler for task "

    invoke-direct {v2, v3}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    .line 122
    invoke-virtual {p0}, Ljava/lang/Object;->hashCode()I

    move-result v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v3, " (context is null)"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 121
    invoke-static {v0, v2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 123
    invoke-virtual {p0, v1}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->cancel(Z)Z

    return-void

    .line 127
    :cond_0
    iget-boolean v2, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->contextIsDroidFuActivity:Z

    if-eqz v2, :cond_3

    .line 128
    move-object v2, v0

    check-cast v2, Landroid/app/Activity;

    .line 129
    iget v3, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->dialogId:I

    const/4 v4, -0x1

    if-le v3, v4, :cond_1

    .line 130
    iget v3, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->dialogId:I

    invoke-virtual {v2, v3}, Landroid/app/Activity;->showDialog(I)V

    .line 132
    :cond_1
    iget-boolean v3, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->isTitleProgressEnabled:Z

    if-eqz v3, :cond_2

    .line 133
    invoke-virtual {v2, v1}, Landroid/app/Activity;->setProgressBarVisibility(Z)V

    goto :goto_0

    .line 134
    :cond_2
    iget-boolean v3, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->isTitleProgressIndeterminateEnabled:Z

    if-eqz v3, :cond_3

    .line 135
    invoke-virtual {v2, v1}, Landroid/app/Activity;->setProgressBarIndeterminateVisibility(Z)V

    .line 138
    :cond_3
    :goto_0
    invoke-virtual {p0, v0}, Lcom/github/droidfu/concurrent/BetterAsyncTask;->before(Landroid/content/Context;)V

    return-void
.end method

.method public setCallable(Lcom/github/droidfu/concurrent/BetterAsyncTaskCallable;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/github/droidfu/concurrent/BetterAsyncTaskCallable<",
            "TParameterT;TProgressT;TReturnT;>;)V"
        }
    .end annotation

    .line 234
    iput-object p1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->callable:Lcom/github/droidfu/concurrent/BetterAsyncTaskCallable;

    return-void
.end method

.method public useCustomDialog(I)V
    .locals 0

    .line 243
    iput p1, p0, Lcom/github/droidfu/concurrent/BetterAsyncTask;->dialogId:I

    return-void
.end method
