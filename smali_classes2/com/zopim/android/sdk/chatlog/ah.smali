.class Lcom/zopim/android/sdk/chatlog/ah;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Z

.field final synthetic b:Lcom/zopim/android/sdk/model/Profile;

.field final synthetic c:Landroid/widget/EditText;

.field final synthetic d:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;ZLcom/zopim/android/sdk/model/Profile;Landroid/widget/EditText;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ah;->d:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    iput-boolean p2, p0, Lcom/zopim/android/sdk/chatlog/ah;->a:Z

    iput-object p3, p0, Lcom/zopim/android/sdk/chatlog/ah;->b:Lcom/zopim/android/sdk/model/Profile;

    iput-object p4, p0, Lcom/zopim/android/sdk/chatlog/ah;->c:Landroid/widget/EditText;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    iget-boolean p1, p0, Lcom/zopim/android/sdk/chatlog/ah;->a:Z

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ah;->b:Lcom/zopim/android/sdk/model/Profile;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Profile;->getEmail()Ljava/lang/String;

    move-result-object p1

    goto :goto_0

    :cond_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ah;->c:Landroid/widget/EditText;

    invoke-virtual {p1}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    :goto_0
    sget-object v0, Landroid/util/Patterns;->EMAIL_ADDRESS:Ljava/util/regex/Pattern;

    invoke-virtual {v0, p1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/regex/Matcher;->matches()Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ah;->d:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$300(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->setEmail(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ah;->d:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$300(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->emailTranscript(Ljava/lang/String;)Z

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ah;->d:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$300(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object p1

    invoke-interface {p1}, Lcom/zopim/android/sdk/api/Chat;->endChat()V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ah;->d:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$1000(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/app/AlertDialog;

    move-result-object p1

    invoke-virtual {p1}, Landroid/app/AlertDialog;->dismiss()V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ah;->d:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$800(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ah;->d:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$900(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/prechat/ChatListener;

    move-result-object p1

    if-eqz p1, :cond_2

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ah;->d:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$900(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/prechat/ChatListener;

    move-result-object p1

    invoke-interface {p1}, Lcom/zopim/android/sdk/prechat/ChatListener;->onChatEnded()V

    goto :goto_1

    :cond_1
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ah;->c:Landroid/widget/EditText;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ah;->d:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$string;->email_transcript_email_message:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getText(I)Ljava/lang/CharSequence;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/widget/EditText;->setError(Ljava/lang/CharSequence;)V

    :cond_2
    :goto_1
    return-void
.end method
