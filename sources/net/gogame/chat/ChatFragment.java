package net.gogame.chat;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnKeyListener;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.TextView.OnEditorActionListener;
import android.widget.Toast;
import com.zopim.android.sdk.C0784R;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import net.gogame.zopim.client.base.ZopimMainActivity;
import org.apache.commons.lang3.StringUtils;

public class ChatFragment extends Fragment {
    public static final int PICK_IMAGE_RESULT_CODE = 5001;
    private ChatAdapter chatAdapter;

    @Nullable
    public View onCreateView(LayoutInflater layoutInflater, @Nullable ViewGroup viewGroup, @Nullable Bundle bundle) {
        this.chatAdapter = ((ZopimMainActivity) getActivity()).getChatAdapter();
        View inflate = layoutInflater.inflate(C0784R.layout.net_gogame_chat_fragment_chat, viewGroup, false);
        ListView listView = (ListView) inflate.findViewById(C0784R.id.listView);
        listView.setAdapter(this.chatAdapter);
        listView.setDivider(null);
        final EditText editText = (EditText) inflate.findViewById(C0784R.id.editText);
        editText.setImeOptions(4);
        editText.setOnEditorActionListener(new OnEditorActionListener() {
            public boolean onEditorAction(TextView textView, int i, KeyEvent keyEvent) {
                if (i != 4) {
                    return false;
                }
                this.chatAdapter.getChatContext().send(editText.getText().toString());
                editText.setText("");
                return true;
            }
        });
        editText.setOnKeyListener(new OnKeyListener() {
            public boolean onKey(View view, int i, KeyEvent keyEvent) {
                if (keyEvent.getAction() != 0 || i != 66) {
                    return false;
                }
                this.chatAdapter.getChatContext().send(editText.getText().toString());
                editText.setText("");
                return true;
            }
        });
        final ImageButton imageButton = (ImageButton) inflate.findViewById(C0784R.id.sendButton);
        imageButton.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (ChatFragment.this.isSendButton(imageButton, editText.getText())) {
                    this.chatAdapter.getChatContext().send(editText.getText().toString());
                    editText.setText("");
                    return;
                }
                Intent intent = new Intent();
                intent.setType("image/*");
                intent.setAction("android.intent.action.GET_CONTENT");
                ChatFragment.this.startActivityForResult(Intent.createChooser(intent, "Select picture"), ChatFragment.PICK_IMAGE_RESULT_CODE);
            }
        });
        editText.addTextChangedListener(new TextWatcher() {
            public void beforeTextChanged(CharSequence charSequence, int i, int i2, int i3) {
            }

            public void onTextChanged(CharSequence charSequence, int i, int i2, int i3) {
            }

            public void afterTextChanged(Editable editable) {
                ChatFragment.this.updateSendButton(imageButton, editable);
            }
        });
        updateSendButton(imageButton, editText.getText());
        return inflate;
    }

    private boolean isSendButton(ImageButton imageButton, Editable editable) {
        boolean isAttachmentSupported;
        ChatContext chatContext = null;
        if (this.chatAdapter != null) {
            chatContext = this.chatAdapter.getChatContext();
        }
        if (chatContext != null) {
            isAttachmentSupported = chatContext.isAttachmentSupported();
        } else {
            isAttachmentSupported = false;
        }
        if (isAttachmentSupported && StringUtils.trimToNull(editable.toString()) == null) {
            return false;
        }
        return true;
    }

    private void updateSendButton(ImageButton imageButton, Editable editable) {
        if (isSendButton(imageButton, editable)) {
            imageButton.setBackgroundResource(C0784R.drawable.net_gogame_chat_send_message_icon);
        } else {
            imageButton.setBackgroundResource(C0784R.drawable.net_gogame_chat_attachment_icon);
        }
    }

    public void onActivityResult(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
        if (i != PICK_IMAGE_RESULT_CODE || i2 != -1) {
            return;
        }
        if (intent == null) {
            Log.e(Constants.TAG, "No data");
        } else {
            send(intent.getData());
        }
    }

    private void send(Uri uri) {
        OutputStream fileOutputStream;
        try {
            String filename = ContentUtils.getFilename(getActivity(), uri);
            if (filename == null) {
                throw new IOException("Cannot determine filename for " + uri);
            }
            File file = new File(getActivity().getCacheDir(), filename);
            InputStream openInputStream = getActivity().getContentResolver().openInputStream(uri);
            try {
                fileOutputStream = new FileOutputStream(file);
                IOUtils.copy(openInputStream, fileOutputStream);
                IOUtils.closeQuietly(fileOutputStream);
                if (file.length() > Constants.MAX_IMAGE_SIZE) {
                    Toast.makeText(getActivity(), C0784R.string.net_gogame_chat_image_too_big_message, 1).show();
                    IOUtils.closeQuietly(openInputStream);
                    return;
                }
                this.chatAdapter.getChatContext().registerImage(file.getName(), Uri.parse(file.toURI().toString()));
                this.chatAdapter.getChatContext().send(file);
                IOUtils.closeQuietly(openInputStream);
            } catch (Throwable th) {
                IOUtils.closeQuietly(openInputStream);
            }
        } catch (IOException e) {
            Toast.makeText(getActivity(), C0784R.string.net_gogame_chat_error_sending_picture_message, 1).show();
        }
    }
}
