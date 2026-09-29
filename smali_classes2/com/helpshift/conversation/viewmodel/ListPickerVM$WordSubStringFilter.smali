.class Lcom/helpshift/conversation/viewmodel/ListPickerVM$WordSubStringFilter;
.super Lcom/helpshift/conversation/viewmodel/ListPickerVM$BaseOptionFilter;
.source "ListPickerVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/viewmodel/ListPickerVM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = "WordSubStringFilter"
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/viewmodel/ListPickerVM;)V
    .locals 1

    .line 320
    iput-object p1, p0, Lcom/helpshift/conversation/viewmodel/ListPickerVM$WordSubStringFilter;->this$0:Lcom/helpshift/conversation/viewmodel/ListPickerVM;

    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Lcom/helpshift/conversation/viewmodel/ListPickerVM$BaseOptionFilter;-><init>(Lcom/helpshift/conversation/viewmodel/ListPickerVM;Lcom/helpshift/conversation/viewmodel/ListPickerVM$1;)V

    return-void
.end method


# virtual methods
.method getRegex(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    .line 323
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "\\B"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method
